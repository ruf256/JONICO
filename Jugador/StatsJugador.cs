using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StatsJugador : MonoBehaviour, IDamageable
{
    public StatsJugadorData data;
    private AudioSource damageAudioSource;

    private void Start()
    {
        data.health = data.maxHealth;
        StateManager.Instance.CambioEstado += CambioEstado;
        damageAudioSource = GetComponent<AudioSource>();
    }

    private void CambioEstado(object sender, EventArgs e)
    {
        data.health = data.maxHealth;
    }

    public void TakeDamage(float damage)
    {
        data.health -= damage;
        if(damageAudioSource.clip != null && !damageAudioSource.isPlaying) damageAudioSource.Play();

        if(data.health <= 0)
        {
            if(StateManager.Instance.isDorico())
            {
                transform.position = Vector3.one;
                data.health = data.maxHealth;
            }

            else
            {
                SceneManager.LoadScene(0);

            }
            
        }

    }


}
