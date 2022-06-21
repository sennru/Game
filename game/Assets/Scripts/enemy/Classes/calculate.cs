using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calculate
{
    Degree degree;
    public float[] position(float radius)
    {
        float[] pos = new float[3];
        GameObject enemy = GameObject.Find("enemy");

        degree = new Degree();
        float y_rot, z_rot;
        float x, y, z;
        y_rot = degree.RandomDegree(0f, 45.0f);
        z_rot = degree.RandomDegree(0f, 45.0f);

        x = radius * Mathf.Sin(z_rot) * Mathf.Cos(y_rot);
        z = radius * Mathf.Sin(z_rot) * Mathf.Sin(y_rot);
        y = radius * Mathf.Cos(z_rot);

        pos[0] = x;
        pos[1] = y;
        pos[2] = z;
        return pos;
    }
}
