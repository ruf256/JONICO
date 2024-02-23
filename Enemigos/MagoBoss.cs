using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagoBoss : MonoBehaviour
{
    private bool stage2;

    SpriteRenderer spRenderer;

    [SerializeField] Sprite[] sprites;

    [SerializeField] GameObject rayo;

    private bool cargandoAtaque;
    private bool atacando;

    private int spriteCounter;

    [SerializeField] float velocity;

    private Transform jugadorPos;

    private float timerCarga;
    [SerializeField] private float cdCarga;

    private float timerAtaque;
    [SerializeField] private float cdAtaque;

    private void Awake()
    {
        jugadorPos = FindAnyObjectByType<MovimientoJugador>().transform;
        spRenderer = GetComponent<SpriteRenderer>();
        spRenderer.sprite = sprites[0];
        stage2 = false;
        rayo.SetActive(false);
        cargandoAtaque = true;
    }


    // Update is called once per frame
    void Update()
    {

        transform.RotateAround(jugadorPos.position, Vector3.up, velocity * Time.deltaTime);

        if (transform.position.y < 65)
        {
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, 65, velocity * Time.deltaTime), transform.position.z);
        }
        if (!stage2) return;

        if(cargandoAtaque)
        {
            if (timerCarga > cdCarga)
            {
                cargandoAtaque = false;
                atacando = true;
                spRenderer.sprite = sprites[4];
                timerCarga = 0;
            }
            else if (timerCarga >= cdCarga / 2)
            {
                spRenderer.sprite = sprites[3];
            }
            else
            {
                spRenderer.sprite = sprites[2];
            }

            timerCarga += Time.deltaTime;
        } 
        else if (atacando)
        {

            timerAtaque += Time.deltaTime;
            if(!rayo.activeSelf) rayo.SetActive(true);

            if(timerAtaque > cdAtaque)
            {
                cargandoAtaque = true;
                rayo.SetActive(false);
                atacando = false;
                timerAtaque = 0;
            }

        }


        
    }

    public void Stage2()
    {
        spRenderer.sprite = sprites[1];
        stage2 = true;
    }
}
