using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocoMotion : MonoBehaviour
{
    InputControler inpCon;

    Vector3 movDir;
    
    Transform cameraObject;

    Rigidbody rb;

    public float moveSpeed = 7;
    public float rotationSpeed = 15;

    Animator anim;

    private void Awake()
    {
        inpCon = GetComponent<InputControler>();
        rb = GetComponent<Rigidbody>();
        cameraObject = Camera.main.transform;
    }
    private void HandleMovement()
    {
        movDir = cameraObject.forward * inpCon.vertInp;
        movDir = movDir + cameraObject.right * inpCon.horInp;
        movDir.Normalize();
        movDir.y = 0;
        movDir = movDir * moveSpeed;

        Vector3 moveVel = movDir;
        rb.velocity = moveVel * Time.deltaTime;
    }

    private void HandleRotation()
    {
        Vector3 trgtDir = Vector3.zero;

        trgtDir = cameraObject.forward * inpCon.vertInp;
        trgtDir = trgtDir + cameraObject.right * inpCon.horInp;
        trgtDir.Normalize();
        trgtDir.y = 0;

        Quaternion trgtRot = Quaternion.LookRotation(trgtDir);
        Quaternion playerRt = Quaternion.Slerp(transform.rotation, trgtRot, rotationSpeed * Time.deltaTime);

        transform.rotation = playerRt;
    }

    public void AllMovement()
    {
        HandleMovement();
        HandleRotation();
    }
}
