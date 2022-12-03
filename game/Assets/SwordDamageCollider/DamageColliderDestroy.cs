using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageColliderDestroy : MonoBehaviour
{
    public float DestroySeconds;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Eliminate());
    }

    IEnumerator Eliminate()
    {
        yield return new WaitForSeconds(DestroySeconds);
        Destroy(gameObject);
    }
}
