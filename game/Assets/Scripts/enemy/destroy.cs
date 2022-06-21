using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroy : MonoBehaviour
{
    AppearTime appeartime;
    float[,] a = new float[3, 2]{
        {-0.02666f, 2.0f},
        {-0.07f, 3.3f},
        {-0.01f, 0.9f},
    };
    float time, appear, disappearTime;

    public float disappearance()
    {
        appear = appeartime.timefunction(a[0, 0], a[0, 1], time);
        return appear;
    }

    void Start()
    {
        time = 0;
        disappearTime = 0;
    }

    void Update() {
        time = Time.deltaTime;
        disappearTime += Time.deltaTime;

        appeartime = new AppearTime();
        appeartime.timefunction(a[0, 0], a[0, 1], time);

        if (disappearTime > disappearance())
        {
            Destroy(this.gameObject);
            disappearTime = 0f;
        }
    }
}
