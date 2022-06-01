using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hit : MonoBehaviour
{
    int score = 0;
    public GameObject collapse;
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "enemy")
        {
            Destroy(collapse);
            Destroy(this.gameObject);
            score += 5;
            Debug.Log(score);
        }
        else if (collision.gameObject.name == "wall")
        {
            Destroy(this.gameObject);
            score -= 5;
            Debug.Log(score);
        }

    }
}
