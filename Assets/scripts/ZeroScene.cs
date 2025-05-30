using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ZeroScene : MonoBehaviour
{
    public GameObject RobotPnl;
    public GameObject OuterButtons;
    public GameObject PauseButton;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0.0f;
        RobotPnl.SetActive(false);
        OuterButtons.SetActive(false);
        PauseButton.SetActive(false);

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
