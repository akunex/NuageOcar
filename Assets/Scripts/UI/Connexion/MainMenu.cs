using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public InputField pseudo;
    public InputField password;
    public Text error;

    void Start()
    {
        error.enabled = false;
    }
    public void PlayGame()
    {
        if (pseudo.text == "Beta" && password.text == "test")
        {
            SceneManager.LoadScene("ZoneCombat");
            error.enabled = false;
        }
        else
        {
            error.enabled = true;
        }
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
