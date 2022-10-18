using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolaDamage : MonoBehaviour
{
    public int damage;
    public GameObject Enemigo;

   private void OnTriggerEnter(Collider other) 
    {
    if (other.tag == "OniSamurai") 
        {
            Enemigo.GetComponent<DatosEnemigo>().VidaEnemigo -= damage; 
        }
    }
}
