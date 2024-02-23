using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<IDamageable>().TakeDamage(33);
        }
        else
        {
            other.gameObject.SetActive(false);
        }
        
    }
}
