using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Degree
{
    public float RandomDegree(float min, float max)
    {
        float deg = Random.Range(min, max);
        return deg * Mathf.Deg2Rad;
    }
}
