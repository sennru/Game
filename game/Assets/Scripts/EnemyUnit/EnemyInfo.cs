using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public struct Enemyinfo
{
    //敵のステータス一覧
    public float Hp;
    public float Damage;
    public float appear;
    public int getScore;
    public float speed;
    public Enemyinfo(float Hp, float Damage, float appear, int getScore, float speed)
    {
        this.Hp = Hp;
        this.Damage = Damage;
        this.appear = appear;
        this.getScore = getScore;
        this.speed = speed;
    }
}
