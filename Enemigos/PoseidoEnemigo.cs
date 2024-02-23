using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoseidoEnemigo : EnemigoBase
{

    Transform jugador;

    public float tiempoChoque = 1f; // Tiempo durante el cual el objeto chocará con el jugador



    void Awake()
    {
        jugador = FindAnyObjectByType<MovimientoJugador>().GetComponent<Transform>();
        health = maxHealth;
        spRenderer = GetComponent<SpriteRenderer>();

        offset = new Vector3(Random.Range(-7.5f, 7.5f), 0, 0);
    }

    private void OnEnable()
    {
        spRenderer.color = Color.white;
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 jugadorPos = jugador.position;
        jugadorPos.y = 3.75f;

        if (Vector3.Distance(transform.position, jugadorPos) > 5.5f)
        {
            
            transform.position = Vector3.MoveTowards(transform.position, jugadorPos + offset, velocity * Time.deltaTime);
        }
        else
        {
            jugadorPos.y = 1;
            transform.position = Vector3.MoveTowards(transform.position, jugadorPos, velocity * Time.deltaTime);
        }
    }


}

