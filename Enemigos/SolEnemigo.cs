using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolEnemigo : EnemigoBase
{

    private void Awake()
    {
        health = maxHealth;
        spRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        if (StateManager.Instance != null)
            StateManager.Instance.CambioEstado += CambioEstado;
        health = maxHealth;
        transform.LookAt(Camera.main.transform);
        spRenderer.color = Color.white;
    }

    private void CambioEstado(object sender, System.EventArgs e)
    {
        if (StateManager.Instance.isDorico())
        {
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        if (StateManager.Instance != null)
            StateManager.Instance.CambioEstado -= CambioEstado;
    }

    private void Update()
    {
        if (transform.position.y < 50)
        {
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, 50, velocity * Time.deltaTime), transform.position.z);
        }

        if(transform.localScale != Vector3.one * 4)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * 4, velocity * Time.deltaTime);
        }
    }
}
