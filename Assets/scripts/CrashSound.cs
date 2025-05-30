using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class CrashSound : MonoBehaviour
{

    public Transform CollectCrashSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag ==("Element"))
        {
            CollectingSoundOn(collision.transform.position);
        }
        else if (collision.gameObject.tag == ("Flask"))
        {
            CollectingSoundOn(collision.transform.position);
        }
        


    }

    void CollectingSoundOn(UnityEngine.Vector3 pos)
    {
        Transform obj = Instantiate(CollectCrashSound, pos, new UnityEngine.Quaternion());
        obj.gameObject.SetActive(true);
    }
}
