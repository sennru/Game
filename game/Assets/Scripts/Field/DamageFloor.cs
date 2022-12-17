using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFloor : MonoBehaviour
{
    WorldTime Seconds;
    GameObject Particle;

    private void Start()
    {
        Seconds = GameObject.Find("WorldTimer").GetComponent<WorldTime>();
        Particle = transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        if(Seconds.WorldTimeSeconds < 300f)
        {
            gameObject.tag = "Untagged";
            Particle.SetActive(false);
        }
        else
        {
            gameObject.tag = "DamageBound";
            gameObject.GetComponent<Renderer>().material.color = Color.red;
            Particle.SetActive(true);
        }
    }
}
