using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwordSkill : MonoBehaviour
{
    public GameObject SwordSkillObject;
    public GameObject[] katanaIsOn;
    public GameObject SwordIsOn;
    protected Slider SkillSlider;
    Animator SliderMovement;
    float sliderspeed;
    protected float Value;
    protected EnergyManager energyManager;
    protected PropertyManager param;

    public void Start()
    {
        SkillSlider = SwordSkillObject.GetComponent<Slider>();
        SliderMovement = SwordSkillObject.GetComponent<Animator>();
        SwordSkillObject.SetActive(false);
        param = GameObject.Find("ParamatorManager").GetComponent<PropertyManager>();
        energyManager = GameObject.Find("EnergyManager").GetComponent<EnergyManager>();
    }


    private void Update()
    {
        SwordSkillDefinition();
        if (EnergyLimit(param.state.property.swordCost[0]))
        {
            SwordSkill1();
        }
        if (EnergyLimit(param.state.property.swordCost[1]))
        {
            SwordSkill2();
        }
        if (EnergyLimit(param.state.property.swordCost[2]))
        {
            SwordSkill3();
        }
    }
    public void SwordSkillDefinition()
    {
        if(katanaIsOn[0].activeSelf == false && SwordIsOn.activeSelf == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                SwordSkillObject.SetActive(true);
                if (katanaIsOn[2].activeSelf == true)//技選択がレベル３の時
                {
                    sliderspeed = 1.4f; //スライダーの速さ(レベル2の時と比較)
                    SliderMovement.SetFloat("Speed", sliderspeed);
                }
                else if (katanaIsOn[3].activeSelf == true) //技選択がレベル4の時
                {
                    sliderspeed = 1.6f * (1f / 0.3f); //時間減速分の乗算、スライダーの速さ(レベル2の時と比較)
                    SliderMovement.SetFloat("Speed", sliderspeed);
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                SwordSkillObject.SetActive(false);
            }
        }
    }

    bool EnergyLimit(int Limit)
    {
        if(energyManager.Energy >= Limit)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public virtual void SwordSkill1() { }
    public virtual void SwordSkill2() { }
    public virtual void SwordSkill3() { }
}
