using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraMenu : MonoBehaviour
{
    [SerializeField] public Transform jugadorPos;
    [SerializeField] float velocity;

    [SerializeField] Camera playerCam;
    void Start()
    {
        StateManager.Instance.CambioEstado += CambioEstado;
    }

    private void CambioEstado(object sender, EventArgs e)
    {
        Camera.SetupCurrent(playerCam);
        gameObject.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(jugadorPos.position, Vector3.up, velocity * Time.deltaTime);   
    }

}
