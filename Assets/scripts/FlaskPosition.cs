using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlaskPosition : MonoBehaviour
{
    GameObject flask;
    // Start is called before the first frame update
    void Start()
    {
        flask = GameObject.Find("Flask").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.P))
        {
            
        }
    }
}
