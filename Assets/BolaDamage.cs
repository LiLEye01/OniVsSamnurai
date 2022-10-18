using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolaDamage : MonoBehaviour
{
    public int damage;
    public GameObject Enemigo;

   private void OnTriggerEnter(Collider other) 
    {
    if (other.gameObject.CompareTag("OniSamurai")) 
        {
            Debug.Log("Damage");
            //Enemigo.GetComponent<DatosEnemigo>().VidaEnemigo -= damage;
            Enemigo.GetComponent<DatosEnemigo>().enemyDamage(damage);
        }
    }
}
