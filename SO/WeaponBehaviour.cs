using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBehaviour : ScriptableObject
{
    public abstract void Fire(Transform arma, WeaponData wpData);

    public abstract void Fire(Transform arma, WeaponData wpData, ProyectilJugador proyectil, float velocidadDisparo);

    public abstract void Fire(AreaDamageJugador area);

    public abstract void AnimacionAtaque(WeaponData wpData, SpriteRenderer spRenderer, int currentSprite);

    public abstract void AnimacionCooldown(WeaponData wpData, SpriteRenderer spRenderer, int currentSprite);
}
