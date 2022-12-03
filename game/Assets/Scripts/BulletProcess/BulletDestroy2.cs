using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        var x = gameObject.transform.position.x - MyPosition.position.x;
        var y = gameObject.transform.position.y - MyPosition.position.y;
        var z = gameObject.transform.position.z - MyPosition.position.z;
        Radius = Mathf.Sqrt(x * x + y * y + z * z);

        if(Radius > 20f)
        {
            Destroy(gameObject);
        }
    }
}
