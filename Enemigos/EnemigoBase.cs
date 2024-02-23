using System;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public abstract class EnemigoBase : MonoBehaviour, IDamageable
{
    public float health;
    public float maxHealth; 
    public float velocity;
    public float damage;
    [HideInInspector]
    public SpriteRenderer spRenderer;
    [HideInInspector]
    public Vector3 offset;
    public bool invulnerable = false;

    public virtual void TakeDamage(float damage)
    {
        if (invulnerable) return;
        health -= damage;

        float healthRatio = health / maxHealth;

        spRenderer.color = Color.Lerp(Color.red, Color.white, healthRatio);


        if (health <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
            IDamageable damageable = other.GetComponent<IDamageable>();
            if (damageable != null) damageable.TakeDamage(damage);
    }

    private void OnTriggerStay(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null) damageable.TakeDamage(damage);
    }

    private void OnCollisionEnter(Collision other)
    {
            IDamageable damageable = other.collider.GetComponent<IDamageable>();
            if (damageable != null) damageable.TakeDamage(damage);
    }
}
