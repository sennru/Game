using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoremanager : MonoBehaviour
{
    public int score = 0;
    Hit hit;
    public GameObject bullet;
    private GameObject scoreUI;

    void Update()
    {
        scoreUI = bullet;
        hit = scoreUI.GetComponent<Hit>();
        this.gameObject.GetComponent<Text>().text = string.Format("score:{0}", score);
    }

    void Start()
    {
        hit = bullet.GetComponent<Hit>();
    }
}
