using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PropertyManager : MonoBehaviour
{
    public float multiple = 1f;
    public Status state;
    public Enemyinfo[] Enemies = new Enemyinfo[5];
    [System.NonSerialized]
    public float[] SwordDamage = new float[4];
    [System.NonSerialized]
    public float[] GunDamage = new float[3];
    [System.NonSerialized]
    public int[] CostSword = new int[3];
    [System.NonSerialized]
    public int[] CostGun = new int[3];
    void Awake()
    {
        state = loadPlayerData(Application.dataPath + "/save2.json");
        for (int i = 0; i < 4; i++)
        {
            SwordDamage[i] = state.property.swordDamage[i] * multiple;
        }
        for (int i = 0; i < 3; i++)
        {
            GunDamage[i] = state.property.gunDamage[i] * multiple;
        }
        CostSword = state.property.swordCost;
        CostGun = state.property.gunCost;

        for (int i = 0; i < Enemies.Length; i++)
        {
            Enemies[i].Hp = state.status[i].hp;
            Enemies[i].Damage = state.status[i].damage;
            Enemies[i].appear = state.status[i].appear;
            Enemies[i].getScore = state.status[i].score;
            Enemies[i].speed = state.status[i].speed;
        }
    }
    // Start is called before the first frame update
    void Update()
    {
        for (int i = 0; i < 4; i++)
        {
            SwordDamage[i] = state.property.swordDamage[i] * multiple;
        }
        for (int i = 0; i < 3; i++)
        {
            GunDamage[i] = state.property.gunDamage[i] * multiple;
        }
    }

    public Status loadPlayerData(string Path)
    {
        string datastr;
        StreamReader reader;
        reader = new StreamReader(Path);
        datastr = reader.ReadToEnd();
        reader.Close();
        return JsonUtility.FromJson<Status>(datastr);
    }
}


[System.Serializable]
public class Enemyinfo
{
    //敵のステータス一覧
    public float Hp;
    public float Damage;
    public float appear;
    public int getScore;
    public float speed;
}

[System.Serializable]
public class Status
{
    public string dateAndTime;
    public SaveProperty[] status = new SaveProperty[5];
    public DamageAndCost property;
}
