using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    [SerializeField] InputAction _moveInput= null;
    [SerializeField] InputAction _cameraInput = null;
    [SerializeField] InputAction _strafeInput = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        _moveInput.Enable();
        _cameraInput.Enable();
        _strafeInput.Enable();
    }

    private void OnDisable()
    {
        _moveInput.Disable();
        _cameraInput.Disable();
        _strafeInput.Disable();
    }

   public Vector2 MoveInput()
    {
        return _moveInput.ReadValue<Vector2>();
    }

    public float StrafeInput()
    {
        return _strafeInput.ReadValue<float>();
    }

    public Vector2 CameraInput()
    {
        return _cameraInput.ReadValue<Vector2>();
    }
}
