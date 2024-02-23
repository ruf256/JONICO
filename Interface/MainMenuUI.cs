using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] GameObject optionsUI;
    [SerializeField] GameObject menuUI;
    [SerializeField] GameObject juegoUI;

    public void Play()
    {
        StateManager.Instance.StartGame();
        menuUI.SetActive(false);
        juegoUI.SetActive(true);
    }

    public void Options()
    {
        menuUI.SetActive(false);
        optionsUI.SetActive(true);
    }
}
