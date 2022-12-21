using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyinfo
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
        this.getScore = getScore; //スコアとエネルギー加算
        this.speed = speed;
    }
}

public class enemyUnit 
{
    static float i = 1f;
    //public List<Enemyinfo> enemyinfos = new List<Enemyinfo>();
    public Enemyinfo firstEnemy = new Enemyinfo(100f, 1f, 1f, 50, 2.0f);
    public Enemyinfo secondEnemy = new Enemyinfo(200f, 2f, 1.2f, 130, 2.3f);
    public Enemyinfo thirdEnemy = new Enemyinfo(300f, 3f, 1.5f, 220, 2.9f);
    public Enemyinfo lastEnemy = new Enemyinfo(300f, 4f, 1.8f, 300, 3.05f);
    public Enemyinfo boss = new Enemyinfo(10000f, 12f, 0f, 10000, 2.0f);
    /*public Enemyinfo firstEnemy = new Enemyinfo(value[0, 0], value[0, 1], value[0, 2], (int)value[0, 3], value[0, 4]);
    public Enemyinfo secondEnemy = new Enemyinfo(value[1, 0], value[1, 1], value[1, 2], (int)value[1, 3], value[1, 4]);
    public Enemyinfo thirdEnemy = new Enemyinfo(value[2, 0], value[2, 1], value[2, 2], (int)value[2, 3], value[2, 4]);
    public Enemyinfo lastEnemy = new Enemyinfo(value[3, 0], value[3, 1], value[3, 2], (int)value[3, 3], value[3, 4]);
    public Enemyinfo boss = new Enemyinfo(value[4, 0], value[4, 1], value[4, 2], (int)value[4, 3], value[4, 4]);*/
    //敵のステータス初期値
    //引数は左から(Hp,Damage, 出現頻度(s), Score, Speed)


}


