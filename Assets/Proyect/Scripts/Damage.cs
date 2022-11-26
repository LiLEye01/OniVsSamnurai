using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    DatosEnemigo enemy;
    
    int dmg = 25;

    private void Start()
    {
        enemy= GetComponent<DatosEnemigo>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("OniSamurai"))
        {
            enemy.VidaEnemigo -= dmg;
        }
    }
}
