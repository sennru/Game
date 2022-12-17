using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearTower : MonoBehaviour
{
    WorldTime Timer;
    GameObject AppearObject;
    // Start is called before the first frame update
    void Start()
    {
        AppearObject = this.gameObject;
        Timer = GameObject.Find("WorldTimer").GetComponent<WorldTime>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Timer.WorldTimeSeconds >= 180f)
        {
            AppearObject.SetActive(true);
        }
        else
        {
            AppearObject.SetActive(false);
        }
    }
}
