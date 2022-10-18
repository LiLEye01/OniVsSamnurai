using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatosEnemigo : MonoBehaviour
{
    public int VidaEnemigo;

    private void Update() 
    
    {
        if (VidaEnemigo <= 0) 
        {
            Debug.Log("Die Oni");
        }
    }
}
