using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Music : MonoBehaviour
{
    public GameObject spawn;

    
    public AudioSource battle;

    
    public AudioClip batalla;

    public float volumen;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Musica());
        }
    }

    IEnumerator Musica()
    {
        Destroy(spawn);

        yield return new WaitForSeconds(1);

        battle.Play();
    }
}
