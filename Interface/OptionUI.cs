using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour
{
    [SerializeField] Sprite[] spriteList;
    [SerializeField] Image resolutionNumber;
    [SerializeField] Opciones options;
    [SerializeField] Button resButton;

    [SerializeField] GameObject mainMenuUI;

    private int currentRes;
    void OnEnable()
    {
        currentRes = options.GetRes();
        resolutionNumber.sprite = spriteList[currentRes];
    }

    public void CambioRes()
    {
        if(currentRes == 3)
        {
            currentRes = 0;
        }
        else
        {
            currentRes++;
        }
        
        resolutionNumber.sprite = spriteList[currentRes];
        options.SetRes(currentRes);
        ResetButton();
        resButton.enabled = true;
    }

    public void Back()
    {
        gameObject.SetActive(false);
        mainMenuUI.SetActive(true);
    }

    void ResetButton()
    {
        resButton.enabled = false;
       
    }



}
