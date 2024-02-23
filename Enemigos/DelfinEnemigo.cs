using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DelfinEnemigo : EnemigoBase
{
    private Transform jugador;
    private float direccionRotacionRandom;
    [SerializeField] float distanciaSegura;
    [SerializeField] private Sprite[] sprites;
    LunaPoolManager lunaPool;
    private float timer;

    [SerializeField] private float cooldownDìparo;

    [SerializeField] private float alturaSalto; // Define la altura del salto
    private bool estaSaltando = false;

    public GameObject prefabProyectil; // Prefab del proyectil
    public float velocidadDisparo = 10f; // Velocidad del proyectil

    private AudioSource audioSource;
    private bool audioPlayed;

    void Awake()
    {
        health = maxHealth;
        invulnerable = true;
        jugador = GameObject.FindGameObjectWithTag("Player").transform;
        spRenderer = GetComponent<SpriteRenderer>();
        lunaPool = FindAnyObjectByType<LunaPoolManager>();
        audioSource = GetComponent<AudioSource>();

        distanciaSegura += Random.Range(-5, 5);

        direccionRotacionRandom = Random.Range(-5, 5);
        if(direccionRotacionRandom == 0 || direccionRotacionRandom == 0.1f || direccionRotacionRandom == -0.1f)
        {
            direccionRotacionRandom = Random.Range(-5, 5);
        }

        if (direccionRotacionRandom < 0)
        {
            spRenderer.flipX = true;
        }

        
    }

    private void CambioEstado(object sender, System.EventArgs e)
    {
        if (StateManager.Instance.isDorico())
        {
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        if (StateManager.Instance != null)
            StateManager.Instance.CambioEstado += CambioEstado;
        health = maxHealth;
        estaSaltando = false;
        spRenderer.sprite = sprites[0];
        spRenderer.color = Color.white;
        invulnerable = true;
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
        direccion.y = 0;

        if (direccion.magnitude != distanciaSegura)
        {
            float distanciaParaMantener = direccion.magnitude > distanciaSegura ? 1 : -1;
            transform.position = Vector3.MoveTowards(transform.position, transform.position + direccion, distanciaParaMantener * velocity * Time.deltaTime);
        }
        transform.RotateAround(jugadorPos, Vector3.up, velocity * direccionRotacionRandom * Time.deltaTime);


        if (!estaSaltando)
        {
            estaSaltando = true;
            StartCoroutine(Salto());
        }
        else
        {
            timer += Time.deltaTime;
        }

        if(timer > cooldownDìparo)
        {
            DispararProyectil();
            timer = 0;
        }
       

    }

    IEnumerator Salto()
    {
        while (estaSaltando)
        {
            float newY = Mathf.Sin(Time.time) * alturaSalto;
            if (newY > transform.position.y)
            {
                invulnerable = false;
                spRenderer.sprite = sprites[1];
            }
            else
            {
                spRenderer.sprite = sprites[2]; 
                invulnerable = false;
            }

            if(audioPlayed)
            {
                audioSource.Play();
                audioPlayed = true;
            }

            if (newY <= 0.88f)
            {
                estaSaltando = false;
                spRenderer.sprite = sprites[0]; 
                invulnerable = true;
                audioPlayed = false;
                newY = 0.88f;
                yield break;
            }

            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            
            yield return null;
        }

        
    }
    void DispararProyectil()
    {
        Vector3 direccion = (Camera.main.transform.position - transform.position).normalized;

        GameObject proyectil = lunaPool.GetObjFromPool();

        proyectil.transform.position = transform.position;

        proyectil.GetComponent<Rigidbody>().velocity = direccion * velocidadDisparo;

    }
}
