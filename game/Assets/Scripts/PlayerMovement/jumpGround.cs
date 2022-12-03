using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpGround : MonoBehaviour
{
    public bool jumpCount = false;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Field")
        {
            jumpCount = true;
        }
        else
        {
            jumpCount = false;
        }
    }

    private void Update()
    {
        //jumpCount = false;
        Debug.Log(jumpCount);
    }
}
