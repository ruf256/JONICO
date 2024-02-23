using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AumentarOpacidad : MonoBehaviour
{
    public SpriteRenderer spriteRenderer; // El SpriteRenderer al que quieres cambiar la opacidad
    public float duracion = 1f; 

    public void Awake()
    {
        StartCoroutine(CambiarOpacidad());
    }

    IEnumerator CambiarOpacidad()
    {
        float tiempoTranscurrido = 0;

        while (tiempoTranscurrido < duracion)
        {
            tiempoTranscurrido += Time.deltaTime;
            float t = tiempoTranscurrido / duracion;

            Color colorActual = spriteRenderer.color;
            colorActual.a = Mathf.Lerp(colorActual.a, 1, t);
            spriteRenderer.color = colorActual;

            yield return null;
        }
    }
}
