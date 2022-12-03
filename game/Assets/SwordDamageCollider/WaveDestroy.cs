using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveDestroy : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(vanish());
    }

    // Update is called once per frame
    IEnumerator vanish()
    {
        yield return new WaitForSeconds(5f / 20f);
        Destroy(gameObject);
    }
}
