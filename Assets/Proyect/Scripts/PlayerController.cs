using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private InputActionReference movementControl;
    [SerializeField]
    private InputActionReference dashControl;
    [SerializeField]
    private InputActionReference hit;
    [SerializeField]
    private InputActionReference change;
    [SerializeField]
    private InputActionReference block;
    [SerializeField]
    private InputActionReference cure;
    [SerializeField]
    private InputActionReference run;

    private CharacterController controller;

    private Vector3 playerVelocity;

    [SerializeField]
    private bool groundedPlayer;

    public Animator anim;

    [SerializeField]
    private float playerSpeed = 10.0f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.81f;
    [SerializeField]
    private float rotationSpeed = 4f;
    [SerializeField]
    private float damage = 25f;
    [SerializeField]
    private GameObject katana;
    [SerializeField]
    private GameObject katana2;
    [SerializeField]
    private float cures = 5;

    private Transform cameraMain;

    [SerializeField]
    private bool walk = false;

    Vector3 move;
    [SerializeField]
    float dashTime, dashSpeed;

    bool isReady;
    public bool isBlock;

    public ParticleSystem particulas;
    public float vidaJugador = 100;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        cameraMain = Camera.main.transform;
        anim = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        movementControl.action.Enable();
        dashControl.action.Enable();
        hit.action.Enable();
        change.action.Enable();
        block.action.Enable();
        cure.action.Enable();
        run.action.Enable();
    }

    private void OnDisable()
    {
        movementControl.action.Disable();
        dashControl.action.Disable();
        hit.action.Disable();
        change.action.Disable();
        block.action.Disable();
        cure.action.Disable();
        run.action.Disable();  
    }

    public void CambiarVidaMaxima(float vidaMaxima)
    {
        particulas.startLifetime = vidaMaxima;
    }

    void Update()
    {
        vidaJugador = Mathf.Clamp(vidaJugador, 0, 100);

        if (vidaJugador >= 100)
        {
            CambiarVidaMaxima(0.2f);
        }
        else if (vidaJugador < 26)
        {
            CambiarVidaMaxima(1.7f);
        }
        else if (vidaJugador < 51)
        {
            CambiarVidaMaxima(0.85f);
        }
        else if (vidaJugador < 76)
        {
            CambiarVidaMaxima(0.425f);
        }

        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 movement = movementControl.action.ReadValue<Vector2>();
        move = new Vector3(movement.x, 0, movement.y);
        move = cameraMain.transform.forward * move.z + cameraMain.transform.right * move.x;
        move.y = 0f;
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (movement.x != 0 || movement.y != 0 && !isReady)
        {
            anim.SetBool("Walk", true);
        }
        else if (movement.x != 0 || movement.y != 0 && isReady)
        {
            anim.SetBool("WalkKatana", true);
        }
        else
        {
            anim.SetBool("Walk", false);
            anim.SetBool("WalkKatana", false);
        }

        if (hit.action.triggered)
        {
            if (isReady)
            {
                anim.SetTrigger("Attack");

            }
        }

        else if (change.action.triggered)
        {
            if (!isReady)
            {
                anim.SetTrigger("Ready");
                isReady = true;
                katana.SetActive(true);
                katana2.SetActive(false);

            }
            else
            {
                anim.SetTrigger("endFight");
                isReady = false;
                katana.SetActive(false);
                katana2.SetActive(true);
            }
        }

       float guard = block.action.ReadValue<float>();

        if (guard > 0)
        {
            anim.SetBool("Block", true);
            isBlock=true;
        }
        else if(guard <= 0)
        {
            anim.SetBool("Block", false);
            isBlock = false;
        }

        if (cure.action.triggered)
        {
            if(vidaJugador < 100 && cures > 0)
            {
                cures--;
                vidaJugador += 35;
            }
            else if(vidaJugador >= 100 && cures > 0)
            {
                cures--;
            }
        }

        if (run.action.ReadValue<float>() > 0 && !isReady)
        {
            anim.SetBool("Run", true);
            playerSpeed = 15.0f;
        }
        else if (run.action.ReadValue<float>() > 0 && isReady)
        {
            anim.SetBool("SprintKatana", true);
        }
        else if (run.action.ReadValue<float>() == 0)
        {
            anim.SetBool("Run", false);
            anim.SetBool("SprintKatana", false);
            playerSpeed = 10.0f;
        }

        //Dash con corutina
        if (dashControl.action.triggered)
        {
            StartCoroutine(charDash());
            anim.SetBool("Dash", true);
        }
        else
        {
            anim.SetBool("Dash", false);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        if(movement != Vector2.zero)
        {
            float targetAngle = Mathf.Atan2(movement.x, movement.y) * Mathf.Rad2Deg + cameraMain.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0f, targetAngle, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }
    }
    IEnumerator charDash()
    {
        float startTime = Time.time;
        while (Time.time < startTime + dashTime)
        {
            controller.Move(move * dashSpeed * Time.deltaTime);
            yield return null;
        }
    }
}