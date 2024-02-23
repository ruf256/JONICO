using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class AnyKeyQuit : MonoBehaviour
{
    private float timer;
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (Input.anyKeyDown && timer > 2f)
        {
            Application.Quit();
        }
    }
}
