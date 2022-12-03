using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldTime : MonoBehaviour
{
    public float WorldTimeSeconds = 0f;
    Text WorldTimeUI;
    void Start()
    {
        WorldTimeUI = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        WorldTimeSeconds += Time.deltaTime;
        TimerUI();
    }
    void TimerUI()
    {
        string Time, second;
        int minutes;
        if (WorldTimeSeconds % 60 < 10)
        {
            second = "0" + (Mathf.Floor(WorldTimeSeconds) % 60f).ToString();
        }
        else
        {
            second = (Mathf.Floor(WorldTimeSeconds) % 60f).ToString();
        }
        minutes = (int)(Mathf.Floor(WorldTimeSeconds / 60f));
        Time = string.Format("{0}:{1}", minutes, second);
        WorldTimeUI.text = Time;
    }
}
