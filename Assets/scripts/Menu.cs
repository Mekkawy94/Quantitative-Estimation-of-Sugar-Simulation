using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PauseScene : MonoBehaviour
{
    public GameObject MenuPnl;
    public GameObject StepsPnl;
    public GameObject InstructionsPnl;
    //public GameObject FinalResultPnl;
    private Vector3 originalCameraPosition;
    //private Vector3 FakeCameraPosition;


    private bool resume;
    



    // Start is called before the first frame update
    void Start()
    {
        resume = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Pause()
    {
        if (resume == false)
        {
            Time.timeScale = 1f;
            Camera.main.transform.position = new Vector3(4.96700001f, 1.59300005f, 6.88899994f);
        }
        else if (resume == true)
        {
            Time.timeScale = 0f;
            Camera.main.transform.position = new Vector3(100, 100, 100);
        }    
        //MainCamera.enabled = false;
        //PausedCamera.enabled = true;
        MenuPnl.SetActive(resume);
        
        resume = !resume;
    }
    public void Resume()
    {

        Camera.main.transform.position =new Vector3(4.96700001f, 1.59300005f, 6.88899994f);
        //MainCamera.enabled = true;
        //PausedCamera.enabled = false;
        MenuPnl.SetActive(false);
        Time.timeScale = 1f;
       

    }
    public void Exit()
    {
        Application.Quit();
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void StepsWindow()
    {
        MenuPnl.SetActive(false);
        StepsPnl.SetActive(true);

    }
    public void InstructionsWindow()
    {
        MenuPnl.SetActive(false);
        InstructionsPnl.SetActive(true);
    }
   
}
