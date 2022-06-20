using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearTime
{
    double T = 0;
    double x;
    double a = -0.0231;
    float y;


    // Update is called once per frame
    public float Appeartime()
    {
        T += (double)Time.deltaTime;
        x = a * T;
        y = (float)(2 * (double)Mathf.Exp((float)x));
        return y;
    }
}
