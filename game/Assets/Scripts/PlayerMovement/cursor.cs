using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cursor : MonoBehaviour
{
    bool monitor = true;
    // Start is called before the first frame update

    void Start()
    {
        Cursor.visible = false;
    }
    void Update()
    {
        Cursor.lockState = CursorLockMode.Confined;
        if (Input.GetKeyDown(KeyCode.Escape) && monitor == true)
        {
            monitor = false;
        }
        else if (Input.GetKey(KeyCode.Mouse0))
        {

            Cursor.visible = false;
            monitor = true;
            Cursor.lockState = CursorLockMode.None;
        }

    }

}
