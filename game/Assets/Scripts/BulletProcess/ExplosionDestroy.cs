using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TimeDestroy());
    }

    IEnumerator TimeDestroy()
    {
        yield return new WaitForSeconds(3.0f);

        Destroy(gameObject);
    }
}
