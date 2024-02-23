using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEditor;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    [SerializeField] WeaponData[] weaponData;
    [SerializeField] ProyectilJugador proyectilJugador;
    [SerializeField] AreaDamageJugador areaEscopeta;
    SpriteRenderer spriteRenderer;
    WeaponSpriteMovement wpSpriteMov;
    public WeaponData currentWeapon;
    int weaponCounter;
    int spriteCounter;
    private float timerIntermitente;
    private float timerDisparando;
    private float timerCooldown;

    private bool puedeDisparar = true;

    private bool coroutineAtaqueAnimacionStarted;

    private AudioSource armaAudioSource;
    private bool armaCargaHasPlayed;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        wpSpriteMov = GetComponentInChildren<WeaponSpriteMovement>();

        armaAudioSource = GetComponent<AudioSource>();

        weaponCounter = -1;
        spriteCounter = -1;
    }

    private void Start()
    {
        StateManager.Instance.CambioEstado += CambioEstado;
    }

    private void Update()
    {
        if (StateManager.Instance.isEsperando()) return;
        if (Input.GetMouseButton(0) && puedeDisparar) { 
            if(currentWeapon.esDps && timerIntermitente >= currentWeapon.intermicion || currentWeapon.esCarga)
            {
                if (!currentWeapon.seMueveAlReves) wpSpriteMov.movActivo = false;
                else
                {
                    wpSpriteMov.movActivo = true;
                }
                if (currentWeapon.esDps) Fire();
                AnimacionAtaque();
                timerIntermitente = 0; 
            }
            else if (currentWeapon.esDps)
            {
                timerIntermitente += Time.deltaTime;
            }
            else if (!currentWeapon.esDps && !currentWeapon.esCarga)
            {
                Fire();
                AnimacionAtaque();
            }
            timerDisparando += Time.deltaTime;
        }

        if (!puedeDisparar)
        {
            timerCooldown += Time.deltaTime;
        }


        if (Input.GetMouseButtonUp(0))
        {
            if(currentWeapon.spritesAtaque.Length > 1 && !currentWeapon.seMueveAlReves) wpSpriteMov.movActivo = true;
            else { wpSpriteMov.movActivo = false; }

        }

    }

    private void Fire()
    {
        if (!currentWeapon.esProyectilista)
        {
            if (!StateManager.Instance.isDorico())
            {
                currentWeapon.weaponBehaviour.Fire(transform, currentWeapon);
                if (!armaAudioSource.isPlaying && currentWeapon.esDps || !currentWeapon.esDps)
                {
                    armaAudioSource.clip = currentWeapon.disparoSound;
                    armaAudioSource.Play();
                }
            }
            else
            {
                areaEscopeta.damage = currentWeapon.damage;
                currentWeapon.weaponBehaviour.Fire(areaEscopeta);
            }
        }
        else
        {
            
                proyectilJugador.damage = currentWeapon.damage;
                currentWeapon.weaponBehaviour.Fire(transform, currentWeapon, proyectilJugador, currentWeapon.velocidadProyectil);
                armaAudioSource.clip = currentWeapon.disparoSound;
            
            
            if(!armaAudioSource.isPlaying)
            armaAudioSource.Play();

        }
        
    }

    private void AnimacionAtaque()
    {

        if (StateManager.Instance.isDorico())
        {
            StartCoroutine(ActivarYDesactivar(areaEscopeta));
            armaAudioSource.clip = currentWeapon.disparoSound;
            armaAudioSource.Play();
            puedeDisparar = false;
            armaCargaHasPlayed = false;
            StartCoroutine(AnimacionCd());
        }
        else
        {
            if (currentWeapon.esDps || currentWeapon.esCarga)
            {
                if (!coroutineAtaqueAnimacionStarted) StartCoroutine(AnimacionAtaqueDPS());

                if (currentWeapon.cargaSound && !armaCargaHasPlayed)
                {
                    armaCargaHasPlayed = true;
                    armaAudioSource.clip = currentWeapon.cargaSound;
                    armaAudioSource.Play();
                }
            }
            else if (currentWeapon.spritesAtaque.Length > 0)
            {
                currentWeapon.weaponBehaviour.AnimacionAtaque(currentWeapon, spriteRenderer, spriteCounter);
            }
            else
            {
                puedeDisparar = false;
                armaCargaHasPlayed = false;
                StartCoroutine(AnimacionCd());
            }
        }

        

    }

    private void CambioEstado(object sender, EventArgs e)
    {
        CambiarArma();
        if (currentWeapon.spritesAtaque.Length < 1) wpSpriteMov.movActivo = false;
        else
        {
            wpSpriteMov.movActivo = true;
        }

        if (StateManager.Instance.isDorico())
        {
            armaAudioSource.pitch -= 0.75f;
        }
    }

    private void CambiarArma()
    {
        weaponCounter++;
        currentWeapon = weaponData[weaponCounter];
        spriteRenderer.sprite = currentWeapon.spriteIdle;

        if (currentWeapon.disparoSound)
            armaAudioSource.clip = currentWeapon.disparoSound;
    }

    public IEnumerator AnimacionAtaqueDPS()
    {
        coroutineAtaqueAnimacionStarted = true;
        float timer = 2;
        int spriteCounter = 0;
        bool yendo = false;
        while (Input.GetMouseButton(0) && timerDisparando < currentWeapon.duration || Input.GetMouseButton(0) && currentWeapon.esCarga)
        {
            if (timer > 0.5f)
            {
                currentWeapon.weaponBehaviour.AnimacionAtaque(currentWeapon,spriteRenderer, spriteCounter);
                timer = 0;

                if (spriteCounter < currentWeapon.spritesAtaque.Length - 1 && yendo)
                {
                    spriteCounter++;
                }
                else if (spriteCounter > 0 && !yendo && currentWeapon.esDps)
                {
                    spriteCounter--;
                }
                else
                {
                    yendo = !yendo;
                }

            }

            timer += Time.deltaTime;

            yield return null;
        }
        coroutineAtaqueAnimacionStarted = false;
        if (!currentWeapon.seMueveAlReves) wpSpriteMov.movActivo = true;
        else
        {
            wpSpriteMov.movActivo = true;
        }

        if (currentWeapon.esDps)
        {
            StartCoroutine(AnimacionCd());
            puedeDisparar = false;
            if (currentWeapon.cooldownSound)
            {
                armaAudioSource.clip = currentWeapon.cooldownSound;
                armaAudioSource.Play();
            }

        }
        else if (currentWeapon.esCarga)
        {
            if (timerDisparando >= currentWeapon.carga)
            {
                proyectilJugador.gameObject.SetActive(true);
                proyectilJugador.gameObject.transform.parent = transform;
                proyectilJugador.transform.rotation = new Quaternion(0, 0, 0, 0);
                proyectilJugador.transform.localPosition = new Vector3(0, 0, 1.5f);
                proyectilJugador.gameObject.transform.parent = null;
                Fire();
                StartCoroutine(AnimacionCd());
                puedeDisparar = false;
                armaCargaHasPlayed = false;
            }
            else
            {
                spriteRenderer.sprite = currentWeapon.spriteIdle;
                proyectilJugador.gameObject.SetActive(false);
            }
        }
        timerDisparando = 0;
    }

    private IEnumerator AnimacionCd()
    {
        
        float timer = 2;
        int spriteCounter = 0;

        if (armaAudioSource.clip == currentWeapon.disparoSound && armaAudioSource.isPlaying && currentWeapon.esDps)
        {
            armaAudioSource.Stop();
        }


        while (timerCooldown < currentWeapon.cooldown || Input.GetMouseButton(0))
        {
            if (timer > 0.5f)
            {
                
                currentWeapon.weaponBehaviour.AnimacionCooldown(currentWeapon, spriteRenderer, spriteCounter);
                timer = 0;

                if (spriteCounter < currentWeapon.spritesCd.Length - 1)
                {
                    spriteCounter++;
                }
                else
                {
                    spriteCounter = 0;
                }

            }

            timer += Time.deltaTime;
            yield return null;
        }
        puedeDisparar = true;
        if (currentWeapon.seMueveAlReves) wpSpriteMov.movActivo = false;
        spriteRenderer.sprite = currentWeapon.spriteIdle;
        
        timerCooldown = 0;
    }


    IEnumerator ActivarYDesactivar(AreaDamageJugador area)
    {
        area.gameObject.SetActive(true);

        // Espera hasta el próximo frame
        yield return new WaitForSeconds(0.5f);

        area.gameObject.SetActive(false);
    }
}
