using UnityEngine;
using UnityEngine.UI;
using System;

public class TimerUI : MonoBehaviour
{
    Image timer;
    [SerializeField] Sprite[] sprites;
    int spriteCounter = 0;

    float fTimer = 0;

    void Start()
    {
        timer = GetComponentInChildren<Image>();
        
        StateManager.Instance.CambioEstado += CambioEstado;

        spriteCounter = 0;
        timer.sprite = sprites[spriteCounter];
        timer.fillAmount = 1;
    }



    // Update is called once per frame
    void Update()
    {
        fTimer += Time.deltaTime;
        timer.fillAmount =  1.0f - (fTimer / 120);

    }


    private void CambioEstado(object sender, EventArgs e)
    {
        spriteCounter++;
        timer.sprite = sprites[spriteCounter];
        fTimer = 0;
        timer.fillAmount = 1;
        
    }
}
