using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XCameraAngle : MonoBehaviour
{
    public float Sensitivity = 1f;
    const float MAX_TURN_ANGLE = 55f;
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
        if (XRot.x > MAX_TURN_ANGLE && XRot.x < 180f)
        {
            Xmove = 0f;
            XRot.x *= 0.995f;
        }
        if (XRot.x > 180f && XRot.x < 360f - MAX_TURN_ANGLE)
        {
            Xmove = 0f;
            XRot *= 1.005f;
        }
        XRot.x -= Xmove;

        transform.localEulerAngles = XRot;
    }
}
