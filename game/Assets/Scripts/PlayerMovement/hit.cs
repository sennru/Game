using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hit : MonoBehaviour
{
    int score = 0;
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
            score += 5;
            Debug.Log(score);
        }
    }

    void ScoreDeduction()
    {
        Transform myTransform = this.transform;
        Vector3 pos = myTransform.position;

        float x, y, z, r;
        x = Mathf.Pow(pos.x, 2.0f);
        y = Mathf.Pow(pos.y, 2.0f);
        z = Mathf.Pow(pos.z, 2.0f);
        r = Mathf.Sqrt(x + y + z);

        if (r >= 60.0f)
        {
            Destroy(this.gameObject);
            score -= 5;
            Debug.Log(score);
        }
    }

    void Update()
    {
        ScoreDeduction();
    }
}
