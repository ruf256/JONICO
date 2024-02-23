using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectilJugador : MonoBehaviour
{
    [HideInInspector] public float damage;
    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null) damageable.TakeDamage(damage);
    }
}
