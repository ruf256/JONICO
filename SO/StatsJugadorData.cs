using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatsJugadorData", menuName = "SO/StatsJugador", order = 1)]
public class StatsJugadorData : ScriptableObject
{
    public float health;
    public float maxHealth;
    public float initialSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;
    public float cooldownHabilidad;

    public HabilidadBehaviour habilidad;
    public AudioClip habilidadAudio;

}
