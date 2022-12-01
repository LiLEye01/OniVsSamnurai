using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaJugador : MonoBehaviour
{
    private ParticleSystem particulas;
    public float vidaJugador = 100;


    private void Start()
    {
        particulas = GetComponent<ParticleSystem>();
    }

    public void CambiarVidaMaxima(float vidaMaxima)
    {
        particulas.startLifetime = vidaMaxima;
    }

    private void Update()
    {
        vidaJugador = Mathf.Clamp(vidaJugador, 0, 100);

        if (vidaJugador >= 100)
        {
            CambiarVidaMaxima(0.2f);
        }
        else if (vidaJugador < 26)
        {
            CambiarVidaMaxima(1.7f);
        }
        else if (vidaJugador < 51)
        {
            CambiarVidaMaxima(0.85f);
        }
        else if (vidaJugador < 76)
        {
            CambiarVidaMaxima(0.425f);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            vidaJugador -= 25;
        }
    }
}
