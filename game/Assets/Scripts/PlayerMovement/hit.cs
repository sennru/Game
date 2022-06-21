using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    Scoremanager scoremanager;
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
            scoremanager.score += 5;
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
            scoremanager.score -= 5;
        }
    }

    void Start()
    {
        scoremanager = GameObject.Find("ScoreManeger").GetComponent<Scoremanager>();
    }

    void Update()
    {
        ScoreDeduction();
    }
}
