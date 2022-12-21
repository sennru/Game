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
    enemyUnit Unit = new enemyUnit();

    bool BossAppear = true;
    //“GoŒ»‹y‚Ñ‚»‚Ì•p“x‚ÆˆÊ’u
    private void Start()
    {
        WorldTimer = GameObject.Find("WorldTimer");
    }
    public void Clone(GameObject enemy, Vector3 position, float appear, int number)
    {
        sec[number] += Time.deltaTime;
        if (sec[number] > appear && GameObject.FindGameObjectsWithTag("Enemy").Length < 150)
        {
            Instantiate(enemy, pos, Quaternion.identity);
            sec[number] = 0f;
        }
        //ˆê’è‚Ì•b”‚ÅoŒ»
    }

    void firstEnemyState(enemyUnit enemy)
    {
        if (seconds < 60f)
        {
            Clone(Enemy[0], pos, enemy.firstEnemy.appear, 0);
        }
    }
    void secondEnemyState(enemyUnit enemy)
    {
        if (seconds > 20f && seconds < 100f)
        {
            Clone(Enemy[1], pos, enemy.secondEnemy.appear, 1);
        }
    }

    void thirdEnemyState(enemyUnit enemy)
    {
        if (seconds > 40f && seconds < 100f)
        {
            Clone(Enemy[2], pos, enemy.thirdEnemy.appear, 2);
        }
        else if (seconds > 100f)
        {
            Clone(Enemy[2], pos, enemy.thirdEnemy.appear - 0.2f, 2);
        }
    }

    void lastEnemyState(enemyUnit enemy)
    {
        if (seconds > 60f && seconds < 100f)
        {
            Clone(Enemy[3], pos, enemy.lastEnemy.appear, 3);
        }
        else if (seconds > 100f)
        {
            Clone(Enemy[3], pos, enemy.lastEnemy.appear - 0.2f, 3);
        }
    }

    void BossState(enemyUnit enemy)
    {
        if (seconds > 100f && BossAppear == true && GameObject.FindGameObjectsWithTag("Enemy").Length < 150)
        {
            BossAppear = false;
            Clone(Enemy[4], pos, enemy.boss.appear, 4);

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
        //“G‚Ì”­¶ˆÊ’u
        firstEnemyState(Unit);
        secondEnemyState(Unit);
        thirdEnemyState(Unit);
        lastEnemyState(Unit);
        BossState(Unit);
    }
}
