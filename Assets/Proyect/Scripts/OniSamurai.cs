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

    DatosEnemigo _DatosEnemigo;

    public NavMeshAgent agente;
    public float distancia_ataque;
    public float radio_vision;

    public states currentStates;
    public enum states
    {
        Live1,
        Live2,
        Dead,
        Dead2
    }

    // Start is called before the first frame update
    void Start()
    {
        Ani = GetComponent<Animator>();
        target = GameObject.Find("Player");
        _DatosEnemigo = GetComponent<DatosEnemigo>();

    }

    // Update is called once per frame
    void Update()
    {
        switch (currentStates)
        {
            case states.Live1:
                Comportamiento_Enemigo();
                if (_DatosEnemigo.VidaEnemigo <= 0)
                {
                    ChangeState(states.Dead);
                }
                break;

            case states.Dead:
                Debug.Log("muerte1");
                StartCoroutine(Change());
                break;

            case states.Live2:
                Debug.Log("Hola");
                distancia_ataque = 4;
                agente.stoppingDistance = distancia_ataque;
                Comportamiento_Enemigo();
                if (_DatosEnemigo.VidaEnemigo <= 0)
                {
                    ChangeState(states.Dead2);
                }
                break;

            case states.Dead2:
                Debug.Log("Muerto");
                break;
        }
    }

    void ChangeState(states nextState)
    {
        currentStates = nextState;
    }

    IEnumerator Change()
    {
        yield return new WaitForSeconds(2);
        _DatosEnemigo.VidaEnemigo = 100;
        ChangeState(states.Live2);
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
                    agente.speed = 3;
                    speed = 3;
                    Ani.SetBool("Attack", false);
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
                Ani.SetBool("Walk", false);
                agente.speed = 15;
                speed = 15;
                Ani.SetBool("Run", true);
                Ani.SetBool("Attack", false);
            }

            else
            {
                if (!atacando)
                {
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 1);
                    Ani.SetBool("Walk", false);
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.gameObject.GetComponent<PlayerController>().isBlock == true)
        {
            Ani.SetTrigger("Block");
        }
        else if(other.CompareTag("Player") && other.gameObject.GetComponent<PlayerController>().isBlock == false)
        {
            //hace daño
        }
    }
}



