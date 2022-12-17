using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int Score;
    Text GameScore;
    public Text ResultScore;

    void Update()
    {
        GameScore = GameObject.Find("ScoreManager").GetComponent<Text>();
        ResultScore.text = "Score : " + Score.ToString();
        GameScore.text = "Score : " + Score.ToString();
    }
}
