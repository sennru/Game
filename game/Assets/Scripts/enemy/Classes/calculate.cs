using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calculate
{
    Degree degree;
    public void position(float radius)
    {
        GameObject enemy = GameObject.Find("enemy");

        degree = new Degree();
        float y_rot, z_rot;
        float x, y, z;
        y_rot = degree.RandomDegree(0f, 45.0f);
        z_rot = degree.RandomDegree(0f, 45.0f);

        x = radius * Mathf.Sin(z_rot) * Mathf.Cos(y_rot);
        z = radius * Mathf.Sin(z_rot) * Mathf.Sin(y_rot);
        y = radius * Mathf.Cos(z_rot);

        Transform myTranform = enemy.transform;
        Vector3 pos = myTranform.position;

        pos.x = x;
        pos.y = y;
        pos.z = z;

        myTranform.position = pos;//配列で返すプログラムに変更→Instantiate関数に代入
    }
}
