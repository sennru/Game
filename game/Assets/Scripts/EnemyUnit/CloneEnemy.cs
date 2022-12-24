using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CaluculateExtention;

public class CloneEnemy : MonoBehaviour
{
    public GameObject[] Enemy;
    public GameObject WorldTimer;
    float seconds;
    float[] sec = new float[5];
    Vector3 pos;
    PropertyManager Unit;

    bool BossAppear = true;
    //ìGèoåªãyÇ—ÇªÇÃïpìxÇ∆à íu
    private void Start()
    {
        Unit = GameObject.Find("ParamatorManager").GetComponent<PropertyManager>();
        WorldTimer = GameObject.Find("WorldTimer");

        Debug.Log(Unit.Enemies[0].Hp);
    }
    public void Clone(GameObject enemy, float appear, int number)
    {
        sec[number] += Time.deltaTime;
        if (sec[number] > appear && GameObject.FindGameObjectsWithTag("Enemy").Length < 150)
        {
            Instantiate(enemy, pos, Quaternion.identity);
            sec[number] = 0f;
        }
        //àÍíËÇÃïbêîÇ≈èoåª
    }

    void firstEnemyState()
    {
        if (seconds < 60f)
        {
            Clone(Enemy[0], Unit.Enemies[0].appear, 0);
        }
    }
    void secondEnemyState()
    {
        if (seconds > 20f && seconds < 100f)
        {
            Clone(Enemy[1], Unit.Enemies[1].appear, 1);
        }
    }

    void thirdEnemyState()
    {
        if (seconds > 40f && seconds < 100f)
        {
            Clone(Enemy[2], Unit.Enemies[2].appear, 2);
        }
        else if (seconds > 100f)
        {
            Clone(Enemy[2], Unit.Enemies[2].appear - 0.2f, 2);
        }
    }

    void lastEnemyState()
    {
        if (seconds > 60f && seconds < 100f)
        {
            Clone(Enemy[3], Unit.Enemies[3].appear, 3);
        }
        else if (seconds > 100f)
        {
            Clone(Enemy[3], Unit.Enemies[3].appear - 0.2f, 3);
        }
    }

    void BossState()
    {
        if (seconds > 100f && BossAppear == true && GameObject.FindGameObjectsWithTag("Enemy").Length < 150)
        {
            BossAppear = false;
            Clone(Enemy[4], Unit.Enemies[4].appear, 4);

        }
    }


    // Update is called once per frame
    void Update()
    {

        seconds = WorldTimer.GetComponent<WorldTime>().WorldTimeSeconds;
        if (seconds <= 120f)
        {
            pos = PositionCalculate.position(LinearFunctionValue.GetValue(0f, 120f, 30f, 100f, seconds));
        }
        else
        {
            pos = PositionCalculate.position(100f);
        }
        //ìGÇÃî≠ê∂à íu
        firstEnemyState();
        secondEnemyState();
        thirdEnemyState();
        lastEnemyState();
        BossState();
    }
}
