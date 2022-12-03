using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBarDirection : MonoBehaviour
{
    void Update()
    {
        this.transform.rotation = Camera.main.transform.rotation;
    }
}
