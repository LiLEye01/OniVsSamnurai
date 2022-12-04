using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FADE : MonoBehaviour
{
    public Animation animator;
    // Start is called before the first frame update
    void Start()
    {
        //invoke("FADEOUT",2);  
    }

    // Update is called once per frame
    void Update()
    {
        animator.Play("FADELN");
    }
}
