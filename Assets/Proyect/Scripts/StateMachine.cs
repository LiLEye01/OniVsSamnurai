using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    states currentStates;
    enum states
    {
        Live1,
        Live2,
        Dead
    }

    void Start()
    {
        StartCoroutine(FSM());
    }

    IEnumerator FSM()
    {
        while (true)
        {
            yield return StartCoroutine(currentStates.ToString());
        }
    }

    void ChangeState(states nextState)
    {
        currentStates = nextState;
    }

    IEnumerator Live1()
    {
        while (currentStates == states.Live1)
        {
            //todo lo que hara en el state 
        }
        yield return 0;
    }

    IEnumerator Live2()
    {
        while (currentStates == states.Live2)
        {
            //todo lo que hara en el state
        }
        yield return 0;
    }

    IEnumerator Dead()
    {
        while (currentStates == states.Dead)
        {
            //lo que pasara cuando muera
        }
        yield return 0;
    }
   

    // Update is called once per frame
    void Update()
    {
        
    }
}
