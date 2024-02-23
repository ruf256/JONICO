using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

[CreateAssetMenu(fileName = "New Raycast Weapon Behaviour", menuName = "SO/WeaponBehaviour/Raycast")]
public class RaycastWeaponBehaviour : WeaponBehaviour
{
    
    public override void Fire(Transform arma, WeaponData wpData)
    {
        Ray ray = new Ray(arma.position, arma.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, wpData.range, wpData.layer))
        {
            IDamageable damageable = hit.collider.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(wpData.damage);
            }
        }
    }

    public override void AnimacionAtaque(WeaponData wpData, SpriteRenderer spRenderer, int nextSprite)
    {
        spRenderer.sprite = wpData.spritesAtaque[nextSprite];
    }

    public override void AnimacionCooldown(WeaponData wpData, SpriteRenderer spRenderer, int nextSprite)
    {
        spRenderer.sprite = wpData.spritesCd[nextSprite];
    }

    public override void Fire(Transform arma, WeaponData wpData, ProyectilJugador proyectil, float velocidadDisparo)
    {
        throw new System.NotImplementedException();
    }

    public override void Fire(AreaDamageJugador area)
    {
        throw new System.NotImplementedException();
    }
}
