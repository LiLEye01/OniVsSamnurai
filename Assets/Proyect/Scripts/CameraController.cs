using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Scripting.APIUpdating;

[RequireComponent(typeof(InputController))]
public class CameraController : MonoBehaviour
{
    [SerializeField] float _mouseSensitivity = 0f;
    [SerializeField] Transform _cameraAnchor = null;
    Vector2 _rotationCamera =  Vector2.zero;

    InputController _inputController = null;
    void Awake()
    {
        _inputController = GetComponent<InputController>();
    }

    // Update is called once per frame
    void Update()
    {
         MouseCamera();
    }

    public void MouseCamera()
    {
        Vector2 input = _inputController.CameraInput();

        _rotationCamera.y += input.x * _mouseSensitivity * Time.deltaTime;
        _rotationCamera.x -= input.y * _mouseSensitivity * Time.deltaTime;

        _rotationCamera.x = Mathf.Clamp(_rotationCamera.x, -50, 60);
        _cameraAnchor.localRotation = Quaternion.Euler(_rotationCamera);
    }
}
