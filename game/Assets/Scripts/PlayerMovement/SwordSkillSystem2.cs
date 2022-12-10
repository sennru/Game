using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CaluculateExtention;

public class SwordSkillSystem2 : MonoBehaviour
{
    public GameObject Zone, SwordIsOn, katanaIsOn, PlayerTransform;
    EnergyManager energyManager;
    SwordSkill swordSkill;
    DamageAndCostManager param;
    public AudioSource Ready;
    public AudioClip[] IAISound;

    float Value;
    bool ZoneIsOn = true;
    public bool IsSuccess = true;
    public float DamageControl;
    void Start()
    {
        Ready = gameObject.GetComponent<AudioSource>();
        swordSkill = GameObject.Find("SwordSkillManager").GetComponent<SwordSkill>();
        energyManager = GameObject.Find("EnergyManager").GetComponent<EnergyManager>();
        param = GameObject.Find("ParamatorManager").GetComponent<DamageAndCostManager>();
    }

    void Update()
    {
        if (energyManager.Energy >= param.CostSword3)
        {
            if (Input.GetMouseButtonDown(0) && SwordIsOn.activeSelf == true && katanaIsOn.activeSelf == true)
            {
                Ready.clip = IAISound[0];
                Ready.time = 0f;
                Ready.PlayOneShot(IAISound[0]);
            }
            if (Input.GetMouseButtonUp(0) && SwordIsOn.activeSelf == true && katanaIsOn.activeSelf == true)
            {
                Value = swordSkill.SliderValue;
                energyManager.Energy -= param.CostSword3;
                Ready.clip = IAISound[1];
                Ready.time = 0.3f;
                Ready.Play();
                if (Value <= 0.65f && Value >= 0.35f)
                {
                    Zone.transform.localScale = new Vector3(10f, 10f, 10f);
                    StartCoroutine(ZoneAttack());
                    IsSuccess = true;
                }
                else
                {
                    //ç≈ëÂíºåaÉ”7, ç≈è¨íºåaÉ”4.55
                    var x = Mathf.Abs(0.5f - Value);
                    var ZoneScale = LinearFunctionValue.GetValue(0.15f, 0.5f, 7f, 4.55f, x);
                    DamageControl = LinearFunctionValue.GetValue(0.15f, 0.5f, 100f, 80f, x);
                    IsSuccess = false;
                    Zone.transform.localScale = new Vector3(ZoneScale, ZoneScale, ZoneScale);
                    StartCoroutine(ZoneAttack());
                }
            }
        }
    }
    IEnumerator ZoneAttack()
    {
        ZoneIsOn = false;
        Instantiate(Zone, PlayerTransform.transform.position, PlayerTransform.transform.rotation);
        yield return new WaitForSeconds(0.5f);
        ZoneIsOn = true;
    }
}
