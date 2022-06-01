using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Class_test
{
    public Text C_text;
    public void GetText(int time)
    {
        this.C_text = GameObject.Find("Timer").GetComponent<Text>();
        C_text.text = time.ToString();
    }

    public int Timer(float time)
    {
        int count;
        time -= Time.time;
        time = Mathf.Floor(time);
        count = (int)time;
        Debug.Log(1);
        return count;
    }

}
