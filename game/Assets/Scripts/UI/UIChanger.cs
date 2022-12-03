using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIChanger : MonoBehaviour
{
    public GameObject[] Panels;
    public int i;
    //HPがなくなってもゲームオーバーに移動しないデバッグ用
    public bool DebugOnly;
    PlayerUnit PlayerHP;
    // Start is called before the first frame update
    void Start()
    {
        for(int j = 0; j < 4; j++)
        {
            //Debug.Log(TentoTwo(i)[j]);
        }
        PlayerHP = GameObject.Find("Player").GetComponent<PlayerUnit>();
    }

    void Update()
    {
        if (i == 4 && Input.GetKeyDown(KeyCode.Escape))
        {
            i = 6;
            Time.timeScale = 0f;
        }
        else if (i == 6 && Input.GetKeyDown(KeyCode.Escape))
        {
            i = 4;
            Time.timeScale = 1.0f;
        }

        for (int j = 0; j < 4; j++)
        {
            if(TentoTwo(i)[j] == 1)
            {
                Panels[j].SetActive(true);
            }
            else
            {
                Panels[j].SetActive(false);
            }
        }
        
        if(PlayerHP.Hp <= 0 && i == 4 && DebugOnly == false)
        {
            i = 8;
        }
    }


    int[] TentoTwo(int i)
    {
        int[] numbers = new int[4];
        for(int j = 0; j < 4; j++)
        {
            numbers[j] = i % 2;
            i /= 2;
        }
        return numbers;
    }
}
