using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatosEnemigo : MonoBehaviour
{
    public int VidaEnemigo = 500;

    private void Update() 
    
    {
        if (VidaEnemigo <= 0) 
        {
            Debug.Log("Die Oni");
        }
    }

    public void enemyDamage(int d)
    {
        VidaEnemigo -= d;
    }
}
