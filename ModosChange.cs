using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModosChange : MonoBehaviour
{
    [SerializeField] private Image Titulo;
    [SerializeField] private Image ShootDesc;
    [SerializeField] private Image HabilityDesc;

    private Animator anima;

    [SerializeField] Sprite[] titulos;
    [SerializeField] Sprite[] shootDesc;
    [SerializeField] Sprite[] habilityDesc;

    int stateCounter;

    private void Awake()
    {
        anima = GetComponentInChildren<Animator>();
        stateCounter = 0;
        Titulo.sprite = titulos[stateCounter];
        ShootDesc.sprite = shootDesc[stateCounter];
        HabilityDesc.sprite = habilityDesc[stateCounter];
    }

    private void Start()
    {
        StateManager.Instance.CambioEstado += CambioEstado;

    }

    private void CambioEstado(object sender, EventArgs e)
    {
        stateCounter++;
        Titulo.sprite = titulos[stateCounter];
        ShootDesc.sprite = shootDesc[stateCounter];
        HabilityDesc.sprite = habilityDesc[stateCounter];
        anima.Play("Titulo" , -1 , 0);
    }
}
