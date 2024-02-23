using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(menuName="SO/WeaponData")]
public class WeaponData : ScriptableObject
{
    public float damage;
    public float range;
    public float recoil;
    public float cooldown;
    public float duration;
    public float carga;
    public bool esDps;
    public bool esCarga;
    public bool esProyectilista;
    public float velocidadProyectil;
    public float intermicion;
    public LayerMask layer = 6;
    


    public bool seMueveAlReves;
    public Sprite spriteIdle;
    public Sprite[] spritesAtaque;
    public Sprite[] spritesCd;
    public WeaponBehaviour weaponBehaviour;


    public AudioClip disparoSound;
    public AudioClip cargaSound;
    public AudioClip cooldownSound;
}
