using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_appear : MonoBehaviour
{
    Calculate calculate;
    public float r = 1000.0f;
    int i = 0;

    void Start()
    {

    }

    void Update()
    {
        calculate = new Calculate();
        i += 1;
        if(i % 100 == 0)
        {
            calculate.position(r);//自身のポジションを設定する→ポジションを取得
        }

    }
}
