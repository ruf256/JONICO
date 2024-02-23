using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "New Habilidad Behaviour", menuName = "SO/HabilidadBehaviour/Dash")]
public class HabilidadDash : HabilidadBehaviour
{
    public float duracion = 0.1f; // Duraci�n del dash en segundos
    public float distancia = 5.0f; // Distancia que se mover� el jugador

    public override void EjecutarHabilidad(MovimientoJugador jugador)
    {
        jugador.StartCoroutine(Dash(jugador));
    }

    private IEnumerator Dash(MovimientoJugador jugador)
    {
        // Guarda la velocidad original
        float velocidadOriginal = jugador.speed;

        // Aumenta la velocidad para el dash
        jugador.speed = distancia;


        // Espera la duraci�n del dash
        yield return new WaitForSeconds(duracion);

        // Restaura la velocidad original
        jugador.speed = jugador.statsJugador.data.initialSpeed;
    }
}
