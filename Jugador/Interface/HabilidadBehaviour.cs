using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HabilidadBehaviour : ScriptableObject
{
    public abstract void EjecutarHabilidad(MovimientoJugador jugador);
}
