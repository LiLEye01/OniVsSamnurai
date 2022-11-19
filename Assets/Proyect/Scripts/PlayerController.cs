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

    private CharacterController controller;

    private Vector3 playerVelocity;

    [SerializeField]
    private bool groundedPlayer;

    private Animator anim;

    [SerializeField]
    private float playerSpeed = 2.0f;
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

    private Transform cameraMain;

    [SerializeField]
    private bool walk = false;

    Vector3 move;
    [SerializeField]
    float dashTime, dashSpeed;

    bool isReady;

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
    }

    private void OnDisable()
    {
        movementControl.action.Disable();
        dashControl.action.Disable();
        hit.action.Disable();
        change.action.Disable();
        block.action.Disable();
        cure.action.Disable();
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 movement = movementControl.action.ReadValue<Vector2>();
        move = new Vector3( movement.x,0,movement.y );
        move = cameraMain.transform.forward * move.z + cameraMain.transform.right * move.x;
        move.y = 0f;
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (movement.x != 0 || movement.y != 0)
        {
            anim.SetBool("Walk", true);
        }
        else
        {
            anim.SetBool("Walk", false);
        }
        
        if(hit.action.triggered)
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
                
            }
            else
            {
                anim.SetTrigger("endFight");
                isReady=false;
                katana.SetActive(false);
            }
        }

        if (block.action.triggered)
        {
            anim.SetBool("Block", true);
        }
        else
        {
            anim.SetBool("Block", false);
        }

        if (cure.action.triggered)
        {
            //salud incrimenta
        }
           
        //Dash con corutina
        if (dashControl.action.triggered)
        {
            StartCoroutine(charDash());
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