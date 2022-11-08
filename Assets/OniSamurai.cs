using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OniSamurai : MonoBehaviour
{

    public int rutina;
    public float cronometro;
    public Animator Ani;
    public Quaternion angulo;
    public float grado;
    public bool atacando;

    public GameObject target;

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
        if (Vector3.Distance(transform.position, target.transform.position) > 50)
        {
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
                    transform.Translate(Vector3.forward * 1 * Time.deltaTime);
                    Ani.SetBool("Walk", true);

                    break;



            }

        }
        else
        {
            if (Vector3.Distance(transform.position, target.transform.position) > 1 && !atacando) 
            
            { 
                var lookPos = target.transform.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 3);
            Ani.SetBool("Walk", false);

            Ani.SetBool("Run", true);
            transform.Translate(Vector3.forward * 4 * Time.deltaTime);

                Ani.SetBool("Attack", false);
                
        }
            else 
            {
                Ani.SetBool("Walk", false);
                Ani.SetBool("Run", false);

                Ani.SetBool("Attack", true);
                atacando = true;
            }
      }
    }

    public void Final_Ani() 
    {
        Ani.SetBool("Attack", false);
        atacando=false; 
    }
}



