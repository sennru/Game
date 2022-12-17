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

public class enemyUnit
{
    public Enemyinfo firstEnemy = new Enemyinfo(100f, 1f, 1f, 50, 2.0f);
    public Enemyinfo secondEnemy = new Enemyinfo(200f, 2f, 1.2f, 130, 2.3f);
    public Enemyinfo thirdEnemy = new Enemyinfo(300f, 3f, 1.5f, 220, 2.9f);
    public Enemyinfo lastEnemy = new Enemyinfo(300f, 4f, 1.8f, 300, 3.05f);
    public Enemyinfo boss = new Enemyinfo(10000f, 12f, 0f, 10000, 2.0f);
    //敵のステータス初期値
    //引数は左から(Hp,Damage, 出現頻度(s), Score, Speed)
}
