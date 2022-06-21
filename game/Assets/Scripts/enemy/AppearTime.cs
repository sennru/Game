using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearTime : MonoBehaviour
{
    Calculate calculate;
    float[] pos = new float[3];
    float time, appear, fx;
    float appearTime;
    float[,] a = new float[3, 2]
    {
        {-0.02666f, 2.0f},
        {-0.07f, 3.3f},
        {-0.01f, 0.9f},
    };
    float rad = 10.0f;
    public GameObject originObject;
    public float span = 3f;

    public float timefunction(float a, float b, float time)
    {
        fx = a * time + b;
        return fx;
    }

    public float appearance()
    {
        if (time > 0 && time < 30)
        {
            appear = timefunction(a[0, 0], a[0, 1], time);
        }
        else if (time >= 30 && time < 40)
        {
            appear = timefunction(a[1, 0], a[1, 1], time);
        }
        else if (time >= 40 && time <= 60)
        {
            appear = timefunction(a[2, 0], a[2, 1], time);
        }

        return appear;
    }

    void Start()
    {
        time = 0;
        appearTime = 0;
        float[] pos = new float[3];
    }

    void Update()
    {
        calculate = new Calculate();
        time += Time.deltaTime;
        appearTime += Time.deltaTime;
        pos = calculate.position(rad);
        Vector3 position = new Vector3(pos[0], pos[1], pos[2]);
        if (appearTime > appearance())
        {
            Instantiate(originObject, position, Quaternion.identity);
            appearTime = 0f;
        }
    }
}
