using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ChefEnemigo : EnemigoBase
{

    private Transform jugador;
    [SerializeField] float distanciaSegura;
    [SerializeField] float cooldownSpawn;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject spaghetti;

    private float timer;
    [SerializeField] private Sprite[] sprites;
    private float direccionRotacionRandom;
    private AudioSource audioSource;
    [SerializeField] private AudioClip[] clips;


    void Awake()
    {
        health = maxHealth;
        jugador = FindAnyObjectByType<MovimientoJugador>().GetComponent<Transform>();
        spRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        direccionRotacionRandom = Random.Range(-5, 5);
    }

    private void OnEnable()
    {
        if (StateManager.Instance != null)
            StateManager.Instance.CambioEstado += CambioEstado;
        health = maxHealth;
        spRenderer.color = Color.white;
        audioSource.clip = clips[0];
        audioSource.Play();
    }

    private void CambioEstado(object sender, System.EventArgs e)
    {
        if (StateManager.Instance.isDorico())
        {
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        if (StateManager.Instance != null)
            StateManager.Instance.CambioEstado -= CambioEstado;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 jugadorPos = jugador.position;

        Vector3 direccion = jugadorPos - transform.position;

        if (direccion.magnitude != distanciaSegura)
        {
            float distanciaParaMantener = direccion.magnitude > distanciaSegura ? 1 : -1;
            transform.position = Vector3.MoveTowards(transform.position, transform.position + direccion, distanciaParaMantener * velocity * Time.deltaTime);
        }

        transform.RotateAround(jugadorPos, Vector3.up, velocity * direccionRotacionRandom * Time.deltaTime);
        



        if (timer > cooldownSpawn)
        {
            spRenderer.sprite = sprites[2];
            audioSource.clip = clips[1];
            audioSource.Play();
            StartCoroutine(Spawnghetti());
            timer = 0;

        }
        else if (timer >= cooldownSpawn / 2)
        {
            spRenderer.sprite = sprites[1];
        }
        else if (timer >= cooldownSpawn / 4)
        {
            audioSource.clip = clips[0];
            audioSource.Play();
            spRenderer.sprite = sprites[0];
        }


        timer += Time.deltaTime;

    }

    IEnumerator Spawnghetti()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            GameObject spag = Instantiate(spaghetti);
            spag.transform.position = spawnPoints[i].position;
            spag.GetComponent<SpaghettiEnemigo>().jugadorTransform = jugador;
            yield return null;
        }
    }
}
