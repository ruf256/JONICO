using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class StateManager : MonoBehaviour
{

    public static StateManager Instance { get; private set; }

    [SerializeField] StatsJugadorData[] statsJugadorData;
    [SerializeField] StatsJugador statsJugador;
    [SerializeField] Material[] skyboxes;

    [SerializeField] AudioSource musica;

    [SerializeField] PrimerBossManager primerBoss;
    [SerializeField] Volume primerBossVol;

    public EventHandler CambioEstado;

    enum State
    {
        EsperandoParaEmpezar,Jonico,Dorico,Lidio,Mixolidio,Victoria,GameOver
    }

    private State state;

    public float timer;

    void Awake()
    {
        Instance = this;
        timer = 0;
    }

    private void Start()
    {
        state = State.EsperandoParaEmpezar;
        statsJugador.data = statsJugadorData[0];
        RenderSettings.skybox = skyboxes[0];
    }

    void Update()
    {
        switch (state)
        {
            case State.EsperandoParaEmpezar:
                return;
            case State.Lidio:
                if(timer >= 120)
                {
                    state = State.Jonico;
                    statsJugador.data = statsJugadorData[1];
                    RenderSettings.skybox = skyboxes[1];
                    CambioEstado?.Invoke(this, EventArgs.Empty);
                    
                    break;
                }
                else if (Input.GetKeyDown(KeyCode.F1))
                {
                    state = State.Jonico;
                    statsJugador.data = statsJugadorData[1];
                    RenderSettings.skybox = skyboxes[1];
                    CambioEstado?.Invoke(this, EventArgs.Empty);
                    
                }
                break;
            case State.Jonico:
                if (timer >= 240)
                {
                    state = State.Mixolidio;
                    statsJugador.data = statsJugadorData[2];
                    RenderSettings.skybox = skyboxes[2];
                    CambioEstado?.Invoke(this, EventArgs.Empty);
                    
                    break;
                }
                else if (Input.GetKeyDown(KeyCode.F1))
                {
                    state = State.Mixolidio;
                    statsJugador.data = statsJugadorData[2];
                    RenderSettings.skybox = skyboxes[2];
                    CambioEstado?.Invoke(this, EventArgs.Empty);
                    break;
                }
                break;
            case State.Mixolidio:
                if (timer >= 360)
                {
                    state = State.Dorico;
                    statsJugador.data = statsJugadorData[3];
                    RenderSettings.skybox = skyboxes[3];
                    primerBoss.gameObject.SetActive(true);
                    primerBossVol.gameObject.SetActive(true);
                    CambioEstado?.Invoke(this, EventArgs.Empty);
                    
                    break;
                }
                else if (Input.GetKeyDown(KeyCode.F1))
                {
                    state = State.Dorico;
                    statsJugador.data = statsJugadorData[3];
                    RenderSettings.skybox = skyboxes[3];
                    primerBoss.gameObject.SetActive(true);
                    CambioEstado?.Invoke(this, EventArgs.Empty);
                    
                    break;
                }
                break;
            case State.Dorico:
                if (timer >= 480)
                {
                    SceneManager.LoadScene(2);
                    state = State.Victoria;
                    statsJugador.data = statsJugadorData[4];
                    RenderSettings.skybox = skyboxes[4];
                    CambioEstado?.Invoke(this, EventArgs.Empty);
                    
                    break;
                }
                else if (Input.GetKeyDown(KeyCode.F1))
                {
                    SceneManager.LoadScene(2);
                    state = State.Victoria;
                    statsJugador.data = statsJugadorData[4];
                    RenderSettings.skybox = skyboxes[4];
                    CambioEstado?.Invoke(this, EventArgs.Empty);
                    
                    break;
                }
                break;
        
        }
        timer += Time.deltaTime;
    }

    public void StartGame()
    {
        state = State.Lidio;
        CambioEstado?.Invoke(this, EventArgs.Empty);
        musica.Play();
    }

    public bool isEsperando()
    {
        return state == State.EsperandoParaEmpezar;
    }

    public bool isJonico()
    {
        return state == State.Jonico;
    }

    public bool isDorico()
    {
        return state == State.Dorico;
    }

    public bool isLidio()
    {
        return state == State.Lidio;
    }

    public bool isMixolidio()
    {
        return state == State.Mixolidio;
    }

    public bool isGameOver()
    {
        return state == State.GameOver;
    }

    public bool isVictoria()
    {
        return state == State.Victoria;
    }

}
