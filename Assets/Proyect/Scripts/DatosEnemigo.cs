using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DatosEnemigo : MonoBehaviour
{
    public int VidaEnemigo = 100;
    public Slider vidaOni;

    private void Update() 
    
    {
        vidaOni.value = VidaEnemigo;

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
