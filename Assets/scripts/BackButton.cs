using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButton : MonoBehaviour
{
    public GameObject BackedPnl;
    public GameObject MenuPnl;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void backButton()
    {
        Time.timeScale = 0f;
        BackedPnl.SetActive(false);
        MenuPnl.SetActive(true);
        
    }
}
