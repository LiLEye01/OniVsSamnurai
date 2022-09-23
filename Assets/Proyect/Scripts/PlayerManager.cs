using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    InputControler inpCon;
    PlayerLocoMotion playerLoc;

    private void Awake()
    {
        inpCon = GetComponent<InputControler>();
        playerLoc = GetComponent<PlayerLocoMotion>();
    }

    private void Update()
    {
        inpCon.HandleAllInputs();
    }

    private void FixedUpdate()
    {
        playerLoc.AllMovement();
    }
}
