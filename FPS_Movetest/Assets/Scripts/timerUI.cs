using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timerUI : MonoBehaviour
{
    TimeCalculator timeCalculator;
    public float t = 60.0f;
    public void Start()
    {

    }

    public void Update()
    {
        timeCalculator = new TimeCalculator();
        timeCalculator.GetText(timeCalculator.Timer(60.0f));
    }
}
