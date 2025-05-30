using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    public GameObject MenuPnl;
    public GameObject AboutUsPnl;
    public GameObject AboutExperPnl;
    public void startexperimentScene()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1.0f;
    }

    public void startExamScene()
    {
        SceneManager.LoadScene(2);
        Time.timeScale = 1.0f;
    }

    public void QuitApp()
    {
        Application.Quit();
    }
    public void AboutExper()
    {
        MenuPnl.SetActive(false);
        AboutExperPnl.SetActive(true);
    }

    public void AboutUs()
    {
        MenuPnl.SetActive(false);
        AboutUsPnl.SetActive(true);
    }


}
