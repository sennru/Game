using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugOnly : MonoBehaviour
{
    ScoreManager Score;
    EnergyManager energyManager;
    PlayerMove Player;
    public GameObject En;
    [SerializeField] bool IsCheat;

    void Start()
    {
        Score = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        energyManager = En.GetComponent<EnergyManager>();
        Player = GameObject.Find("Player").GetComponent<PlayerMove>();
    }


    void Update()
    {
        if (IsCheat == true)
        {
            if (Input.GetMouseButton(0) && Input.GetKey(KeyCode.LeftShift))
            {
                Time.timeScale = 10f;
            }
            else if (Input.GetMouseButton(0) && Input.GetKey(KeyCode.CapsLock))
            {
                Time.timeScale = 0.3f;
            }
            else
            {
                Time.timeScale = 1.0f;
            }
            if (Input.GetKeyDown("q"))
            {
                energyManager.Energy += 10000;
                for (int i = 0; i < Player.ItemCount.Length; i++)
                {
                    Player.ItemCount[i] += 1;
                }
            }
            if (Input.GetKeyDown("v"))
            {
                Score.Score += 1000;
            }

            if (Input.GetKeyDown("p"))
            {
                foreach(var n in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    Destroy(n);
                }
            }

        }
    }
}
