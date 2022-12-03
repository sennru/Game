using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsDestroy3 : MonoBehaviour
{
    public GameObject Explosion;
    Vector3 Here;
    // Update is called once per frame

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name != "Player")
        {
            Here = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
            Instantiate(Explosion, Here, Quaternion.Euler(0, 0, 0));
            Destroy(gameObject);
        }
    }
}