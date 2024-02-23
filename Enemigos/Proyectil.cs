using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectil : EnemigoBase
{
    float timer = 0;
    [SerializeField] private float tiempoDeVida;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (timer > tiempoDeVida)
        {
            timer = 0;
            gameObject.SetActive(false);
        }
        timer += Time.deltaTime;
    }

    private void OnEnable()
    {
        audioSource.Play(); 
    }

}
