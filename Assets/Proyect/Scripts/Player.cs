using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(InputController))]
public class Player : MonoBehaviour
{
    [SerializeField] float _speed = 0f;
    [SerializeField] float _rotationSpeed = 0f;

    InputController _inputController = null;
    private void Awake()
    {
        _inputController = GetComponent<InputController>(); 
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move()
    {
        Vector2 input = _inputController.MoveInput();
        float strafe = _inputController.StrafeInput();

        transform.position += transform.forward * input.y * _speed * Time.deltaTime;
        transform.position += transform.right * strafe * _speed * Time.deltaTime;
        transform.Rotate(Vector3.up * input.x * _rotationSpeed * Time.deltaTime);
    }

}

