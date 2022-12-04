using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyManager : MonoBehaviour
{
    public Text EnergyText;
    [System.NonSerialized]
    public int Energy = 50;


    void Update()
    {
        EnergyText.text = "Energy : " + Energy.ToString();
    }
}
