using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetFlaskPosition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = new Vector3(5.30700016f, 1.27400005f, 5.43400002f);
        }
        
    }
}
