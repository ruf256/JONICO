using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Habilidad Behaviour", menuName = "SO/HabilidadBehaviour/SaltoRapido")]
public class HabilidadSaltoRapido : HabilidadBehaviour
{
    [SerializeField] private float duracion = 5.0f; // Duración de la habilidad en segundos
    [SerializeField] private float velocidadExtra = 5.0f; // Velocidad extra que se añadirá al saltar


    private IEnumerator AumentarVelocidad(MovimientoJugador jugador)
    {
        // Guarda la velocidad original
        float velocidadOriginal = jugador.speed;

        // Aumenta la velocidad
        jugador.speed += velocidadExtra;

        // Espera la duración de la habilidad
        yield return new WaitForSeconds(duracion);

        // Restaura la velocidad original
        jugador.speed = jugador.statsJugador.data.initialSpeed;
    }

    public override void EjecutarHabilidad(MovimientoJugador jugador)
    {
        jugador.StartCoroutine(AumentarVelocidad(jugador));
    }

}
