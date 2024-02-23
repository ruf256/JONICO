using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opciones : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StateManager.Instance.CambioEstado += CambioEstado;

        if (!PlayerPrefs.HasKey("Resolution")) SetRes(1);
        else
        {
            SetRes(GetRes());  
        }
    }

    private void CambioEstado(object sender, EventArgs e)
    {
        if (StateManager.Instance.isLidio())
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void SetRes(int res)
    {
        PlayerPrefs.SetInt("Resolution", res);
        switch (res)
        {
            case 0:
                Screen.SetResolution(1024, 768, FullScreenMode.FullScreenWindow);
                break;
            case 1:
                Screen.SetResolution(1280, 960, FullScreenMode.FullScreenWindow);
                break;
            case 2:
                Screen.SetResolution(1400, 1050, FullScreenMode.FullScreenWindow);
                break;
            case 3:
                Screen.SetResolution(1600, 1200, FullScreenMode.FullScreenWindow);
                break;
        }
    }

    public int GetRes()
    {
        return PlayerPrefs.GetInt("Resolution");
    }

}
