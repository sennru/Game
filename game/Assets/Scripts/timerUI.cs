using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timerUI : MonoBehaviour
{
    TimeCalculator timeCalculator;
    public float gametime = 60.0f;
    public float currentTime;
    int display;
    public void Start()
    {
        currentTime = gametime;
    }

    public void Update()
    {
        timeCalculator = new TimeCalculator();
        currentTime = timeCalculator.Timer(currentTime);
        display = Mathf.FloorToInt(currentTime);
        timeCalculator.GetText(display);
    }
}
