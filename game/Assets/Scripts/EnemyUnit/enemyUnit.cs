using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyUnit
{
    public Enemyinfo firstEnemy = new Enemyinfo(1000f, 1f, 1f, 50, 2.0f);
    public Enemyinfo secondEnemy = new Enemyinfo(2000f, 2f, 1.2f, 130, 2.3f);
    public Enemyinfo thirdEnemy = new Enemyinfo(3000f, 3f, 1.5f, 220, 2.9f);
    public Enemyinfo lastEnemy = new Enemyinfo(3000f, 4f, 1.8f, 300, 3.05f);
    public Enemyinfo boss = new Enemyinfo(20000f, 12f, 0f, 10000, 2.0f);
    //�G�̃X�e�[�^�X�����l
    //�����͍�����(Hp,Damage, �o���p�x(s), Score, Speed)
}