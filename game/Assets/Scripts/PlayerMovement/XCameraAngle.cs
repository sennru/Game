using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XCameraAngle : MonoBehaviour
{

    void Update()
    {
        if (!Input.GetMouseButton(2))
        {
            Xrot();
        }
    }

    void Xrot()
    {
        float Xmove = Input.GetAxis("Mouse Y");
        Vector3 XRot = transform.localEulerAngles;
        XRot.x -= Xmove;
        transform.localEulerAngles = XRot;
    }
}
