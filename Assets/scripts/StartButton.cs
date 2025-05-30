using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartButton : MonoBehaviour
{
    public GameObject RobotPnl;
    public GameObject StartCanvas;
    public GameObject OuterButtons;
    public GameObject PauseButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startButton()
    {
        StartCanvas.SetActive(false);
        RobotPnl.SetActive(true);
        OuterButtons.SetActive(true);
        PauseButton.SetActive(true);
        Time.timeScale = 1.0f;


    }
}
