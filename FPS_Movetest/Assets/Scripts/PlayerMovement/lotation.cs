using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lotation : MonoBehaviour
{
    // Start is called before the first frame update

    private float rotationSpeed;
    float x = 0;
    float y = 0;


    void Start()
    {
        rotationSpeed = 3.0f;
    }

    void Update()
    {
        float xRot = Input.GetAxis("Mouse X") * rotationSpeed;
        float yRot = Input.GetAxis("Mouse Y") * rotationSpeed;
        
        x += xRot;
        y += yRot;
        transform.localRotation = Quaternion.Euler(-y, x, 0);
    }
}
