using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class AreaDamageJugador : MonoBehaviour
{
    public float damage;

    [SerializeField] AreaDamageJugador[] areas;

    private void Awake()
    {
        gameObject.SetActive(false);
        if(areas.Length> 0)
        {
            areas[0].damage = damage / 2;
            areas[1].damage = damage / 2.25f;
        }
        
    }

    private void OnEnable()
    {

        if (areas.Length > 0)
        {
            areas[0].gameObject.SetActive(true);
            areas[1].gameObject.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damage);
        }
    }

}
