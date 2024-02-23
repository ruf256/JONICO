using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugCustom : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKeyDown(KeyCode.Keypad1)) Application.targetFrameRate = 10;
            if (Input.GetKeyDown(KeyCode.Keypad2)) Application.targetFrameRate = 20;
            if (Input.GetKeyDown(KeyCode.Keypad3)) Application.targetFrameRate = 30;
            if (Input.GetKeyDown(KeyCode.Keypad4)) Application.targetFrameRate = 60;
            if (Input.GetKeyDown(KeyCode.Keypad5)) Application.targetFrameRate = 900;
        }

        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }
}
