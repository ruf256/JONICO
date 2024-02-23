using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaghettiEnemigo : EnemigoBase
{
    
    public float cooldownMovimiento;
    private bool seTransoformo = false;
    private int transformacionConteo = 0;
    private float timer;
    [SerializeField] private Sprite[] sprites;
    private int currentSprite;
    private bool cambioSprite;

    [HideInInspector]
    public Transform jugadorTransform;

    private AudioSource audioSource;

    [SerializeField] private AudioClip[] clips;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        health = maxHealth;
        currentSprite = 0;
        spRenderer = GetComponent<SpriteRenderer>();
        offset = new Vector3(Random.Range(-4.5f, 4.5f), 0, 0);
        

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
        spRenderer.color = Color.white;
        audioSource.clip = clips[0];
        audioSource.Play();
    }

    private void OnDisable()
    {
        if (StateManager.Instance != null)
            StateManager.Instance.CambioEstado -= CambioEstado;
    }

    void Update()
    {
        Vector3 jugadorPos = jugadorTransform.position;
        jugadorPos.y = transform.position.y; 

        if (seTransoformo)
        {
            if(Vector3.Distance(transform.position, jugadorPos) > 5.5f)
            {
                transform.position = Vector3.MoveTowards(transform.position, jugadorPos + offset, velocity * Time.deltaTime);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, jugadorPos, velocity * Time.deltaTime);
            }
            
            return;
        }


        if(timer < cooldownMovimiento)
        {
            if (Vector3.Distance(transform.position, jugadorPos) < 5.5f)
            {
                transform.position = Vector3.MoveTowards(transform.position, jugadorPos + offset, velocity * Time.deltaTime);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, jugadorPos, velocity * Time.deltaTime);
            }
        }
        else if (!cambioSprite)
        {
            currentSprite++;
            spRenderer.sprite = sprites[currentSprite];
            cambioSprite = true;
        }
        else if (timer > cooldownMovimiento * 2)
        {
            timer = 0;
            transformacionConteo++;
            cambioSprite = false;
        }

        if(transformacionConteo == 2 && !seTransoformo)
        {
            seTransoformo = true;
            velocity *= 1.2f;
            audioSource.clip = clips[1];
            audioSource.Play();
        }


        timer += Time.deltaTime;
    }

}
