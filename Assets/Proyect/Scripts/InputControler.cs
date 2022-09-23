using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControler : MonoBehaviour
{
    PlayerControl control;

    public Vector2 movIn;

    
    public float vertInp;
    public float horInp;
    private void OnEnable()
    {
        if (control == null)
        {
            control = new PlayerControl();

            control.PlayerMovement.Movement.performed += i => movIn = i.ReadValue<Vector2>();
        }
        control.Enable();
    }

    private void OnDisable()
    {
        control.Disable();
    }

    public void HandleAllInputs()
    {
        HandleMovInp();
        //aqui se activan todos los handle
    }

    private void HandleMovInp()
    {
        vertInp = movIn.y;
        horInp = movIn.x;
    }
}
