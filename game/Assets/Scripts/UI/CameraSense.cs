using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraSense : MonoBehaviour
{
    PlayerMove Y_Camera;
    XCameraAngle X_Camera;
    Slider SensitivityControl;
    public Text SensitivityValue;
    // Start is called before the first frame update
    void Start()
    {
        Y_Camera = GameObject.Find("Player").GetComponent<PlayerMove>();
        X_Camera = GameObject.Find("Main Camera").GetComponent<XCameraAngle>();
        SensitivityControl = gameObject.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        SensitivityValue.text = (SensitivityControl.value * 2).ToString();
        Y_Camera.Sensitivity = SensitivityControl.value * 2f;
        X_Camera.Sensitivity = SensitivityControl.value * 2f;
    }
}
