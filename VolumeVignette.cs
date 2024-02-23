using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VolumeVignette : MonoBehaviour
{
    Volume vol;
    Vignette vig;
    float initialIntensity;
    [SerializeField] float tiempoAnim;
    void Start()
    {
        vol = GetComponent<Volume>();
        StateManager.Instance.CambioEstado += CambioEstado;
    }

    private void CambioEstado(object sender, EventArgs e)
    {
        Vignette tmp;
        if (vol.profile.TryGet<Vignette>(out tmp))
        {
            vig = tmp;
            initialIntensity = vig.intensity.value;
        }
        StartCoroutine(SmoothAnimationPlus());
    }


    IEnumerator SmoothAnimationPlus()
    {
        float timer = 0; 
        while (tiempoAnim > timer)
        {
            vig.intensity.value += 0.005f;
            timer += Time.deltaTime;
            yield return null;
        }
        StartCoroutine(SmoothAnimationMinus());
    }

    IEnumerator SmoothAnimationMinus()
    {
        while (vig.intensity.value > initialIntensity)
        {
            vig.intensity.value -= 0.005f;
            if(vig.intensity.value < initialIntensity) vig.intensity.value = initialIntensity;
            yield return null;
        }
    }
}
