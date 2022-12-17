using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CaluculateExtention;

public class BulletDestroy2 : MonoBehaviour
{
    Transform MyPosition;
    float Radius;
    private void Start()
    {
        MyPosition = GameObject.Find("Player").transform;
    }
    // Update is called once per frame
    void Update()
    {
        Radius = PositionCalculate.Distance(gameObject.transform.position, MyPosition.transform.position);

        if(Radius > 20f)
        {
            Destroy(gameObject);
        }
    }
}
