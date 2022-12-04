using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XCameraAngle : MonoBehaviour
{
    public float Sensitivity = 1f;
    void Update()
    {
        if (!Input.GetMouseButton(2))
        {
            Xrot();
        }
    }

    void Xrot()
    {
        float Xmove = Input.GetAxis("Mouse Y") * 2 * Sensitivity;
        Vector3 XRot = transform.localEulerAngles;
        XRot.x -= Xmove;
        transform.localEulerAngles = XRot;
    }
}
