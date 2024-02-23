using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Proyectil Weapon Behaviour", menuName = "SO/WeaponBehaviour/Proyectil")]
public class ProyectilWeaponBehaviour : WeaponBehaviour
{
    public override void AnimacionAtaque(WeaponData wpData, SpriteRenderer spRenderer, int nextSprite)
    {
        spRenderer.sprite = wpData.spritesAtaque[nextSprite];
    }

    public override void AnimacionCooldown(WeaponData wpData, SpriteRenderer spRenderer, int nextSprite)
    {
        if (wpData.spritesCd[nextSprite] != null)
        {
            spRenderer.sprite = wpData.spritesCd[nextSprite];
        }
        
    }

    public override void Fire(Transform arma, WeaponData wpData)
    {
        throw new System.NotImplementedException();
    }

    public override void Fire(Transform arma, WeaponData wpData, ProyectilJugador proyectil, float velocidadDisparo)
    {
        proyectil.gameObject.SetActive(true);

        Vector3 direccion = arma.transform.forward;

        proyectil.GetComponent<Rigidbody>().velocity = direccion * velocidadDisparo;
    }

    public override void Fire(AreaDamageJugador area)
    {
        throw new System.NotImplementedException();
    }
}
