using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCalculator
{
    public Text C_text;
    float count;
    public void GetText(int time)
    {
        this.C_text = GameObject.Find("Timer").GetComponent<Text>();
        C_text.text = time.ToString();
    }

    public float Timer(float currentTime)
    {
        currentTime -= Time.deltaTime;
        if (currentTime <= 0f)
        {
            GetText(60);
            currentTime = 60.0f;
        }
        return currentTime;
    }
}
