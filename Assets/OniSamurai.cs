using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class OniSamurai : MonoBehaviour
{

    public int rutina;
    public float cronometro;
    public Animator Ani;
    public Quaternion angulo;
    public float grado;
    public bool atacando;

    public float speed;

    public GameObject target;

    public NavMeshAgent agente;
    public float distancia_ataque;
    public float radio_vision;

    // Start is called before the first frame update
    void Start()
    {
        Ani = GetComponent<Animator>();
        target = GameObject.Find("Player");

    }

    // Update is called once per frame
    void Update()
    {

        Comportamiento_Enemigo();
    }



    public void Comportamiento_Enemigo()
    {
        if (Vector3.Distance(transform.position, target.transform.position) > radio_vision)
        {
            agente.enabled = false;
            Ani.SetBool("Run", false);
            cronometro += 1 * Time.deltaTime;
            if (cronometro >= 4)
            {
                rutina = Random.Range(0, 2);
                cronometro = 0;
            }
            switch (rutina)
            {
                case 0:
                    Ani.SetBool("Walk", false);
                    break;

                case 1:
                    grado = Random.Range(0, 360);
                    angulo = Quaternion.Euler(0, grado, 0);
                    rutina++;
                    break;

                case 2:
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, angulo, 0.5f);
                    transform.Translate(Vector3.forward * speed * Time.deltaTime);
                    Ani.SetBool("Walk", true);

                    break;



            }

        }
        else
        {



            var lookPos = target.transform.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            agente.enabled = true;
            agente.SetDestination(target.transform.position);

            if (Vector3.Distance(transform.position, target.transform.position) > distancia_ataque && !atacando)
            {
                Ani.SetBool("walk", false);
                Ani.SetBool("Run", true);
            }

            else
            {
                if (!atacando)
                {
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 1);
                    Ani.SetBool("walk", false);
                    Ani.SetBool("Run", false);
                    Ani.SetBool("Attack", true);
                }



            }
        }
        if (atacando)
        {
            agente.enabled = false;
        }
    }

    public void Final_Ani()
    {
        if(Vector3.Distance(transform.position,target.transform.position)> distancia_ataque + 0.2f)
        {
            Ani.SetBool("Attack", false);
        }
        
    }
}



