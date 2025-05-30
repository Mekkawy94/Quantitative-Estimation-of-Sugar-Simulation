using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class OutButtons : MonoBehaviour
{
    public GameObject MenuPnl;
    public GameObject StepPnl;
    public GameObject InstructionsPnl;

    private bool resume;
    // Start is called before the first frame update
    void Start()
    {
        resume = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (resume == false)
            {
                StepPnl.SetActive(false);
                InstructionsPnl.SetActive(false);
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
        
    }
}
