using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageAndCostManager : MonoBehaviour
{
    public enum DamageAndCost
    {

    }
    public float SwordDamage1;
    public float SwordDamage2;
    public float SwordDamage3;
    public float SwordDamage4;
    public float GunDamage1;
    public float GunDamage2;
    public float GunDamage3;
    public int CostSword2;
    public int CostSword3;
    public int CostSword4;
    public int CostGun1;
    public int CostGun2;
    public int CostGun3;
    // Start is called before the first frame update
    void Start()
    {
        SwordDamage1 = 50f;
        SwordDamage2 = 200f;
        SwordDamage3 = 120f;
        SwordDamage4 = 120f;
        GunDamage1 = 10f;
        GunDamage2 = 100f;
        GunDamage3 = 800f;
        CostSword2 = 500;
        CostSword3 = 700;
        CostSword4 = 1500;
        CostGun1 = 4;
        CostGun2 = 50;
        CostGun3 = 1000;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
