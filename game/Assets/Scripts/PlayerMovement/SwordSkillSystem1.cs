using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSkillSystem1 : MonoBehaviour
{
    public GameObject ImpactObject;
    public GameObject SwordAction;
    public GameObject SwordIsOn;
    public GameObject KatanaIsOn;
    SwordSkill swordSkill;
    EnergyManager energyManager;
    DamageAndCostManager param;
    public AudioSource Ready;
    public AudioClip[] IAISound;
    float Value;
    void Start()
    {
        param = GameObject.Find("ParamatorManager").GetComponent<DamageAndCostManager>();
        energyManager = GameObject.Find("EnergyManager").GetComponent<EnergyManager>();
        swordSkill = GameObject.Find("SwordSkillManager").GetComponent<SwordSkill>();
    }

    void Update()
    {
        if (energyManager.Energy >= param.CostSword2)
        {
            if (Input.GetMouseButtonDown(0) && SwordIsOn.activeSelf == true && KatanaIsOn.activeSelf == true)
            {
                Ready.clip = IAISound[0];
                Ready.time = 0f;
                Ready.PlayOneShot(IAISound[0]);
            }
            if (Input.GetMouseButtonUp(0) && SwordIsOn.activeSelf == true && KatanaIsOn.activeSelf == true)
            {
                Value = swordSkill.SliderValue;
                Ready.clip = IAISound[1];
                Ready.time = 0.5f;
                Ready.Play();
                if (swordSkill.IsSuccess == true)
                {
                    SwordSkillDefinition(60f);
                }
                else
                {
                    SwordSkillDefinition(20f);
                }
            }
        }
    }

    public void SwordSkillDefinition(float Speed)
    {
        GameObject impact = (GameObject)Instantiate(ImpactObject, transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0));
        Rigidbody impactRb = impact.GetComponent<Rigidbody>();
        impactRb.velocity = SwordAction.transform.forward * Speed;
        energyManager.Energy -= param.CostSword2;
    }
}
