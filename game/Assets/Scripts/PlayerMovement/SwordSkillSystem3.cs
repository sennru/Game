using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CaluculateExtention;

public class SwordSkillSystem3 : MonoBehaviour
{
    public GameObject Zone, SwordIsOn, katanaIsOn, PlayerTransform;
    EnergyManager energyManager;
    SwordSkill swordSkill;
    DamageAndCostManager param;
    float Value;
    bool ZoneIsOn = true;
    public bool IsSuccess = true;
    public float DamageControl;
    public AudioSource Ready;
    public AudioClip[] IAISound;

    void Start()
    {
        Ready = gameObject.GetComponent<AudioSource>();
        swordSkill = GameObject.Find("SwordSkillManager").GetComponent<SwordSkill>();
        energyManager = GameObject.Find("EnergyManager").GetComponent<EnergyManager>();
        param = GameObject.Find("ParamatorManager").GetComponent<DamageAndCostManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (energyManager.Energy >= param.CostSword4)
        {
            if (Input.GetMouseButtonDown(0) && SwordIsOn.activeSelf == true && katanaIsOn.activeSelf == true)
            {
                Time.timeScale = 0.3f;
                Ready.clip = IAISound[0];
                Ready.time = 0f;
                Ready.PlayOneShot(IAISound[0]);
                Ready.clip = IAISound[1];
                Ready.Play();
                Ready.loop = true;
            }
            if (Input.GetMouseButtonUp(0) && SwordIsOn.activeSelf == true && katanaIsOn.activeSelf == true)
            {
                Ready.loop = false;
                Time.timeScale = 1.0f;
                Ready.clip = IAISound[2];
                Ready.Play();
                Value = swordSkill.SliderValue;
                energyManager.Energy -= param.CostSword4;
                if (Value <= 0.65f && Value >= 0.35f)
                {
                    Zone.transform.localScale = new Vector3(15f, 15f, 15f);
                    StartCoroutine(ZoneAttack());
                    IsSuccess = true;
                }
                else
                {
                    //ç≈ëÂíºåaÉ”15, ç≈è¨íºåaÉ”10
                    var x = Mathf.Abs(0.5f - Value);
                    var ZoneScale = LinearFunctionValue.GetValue(0f, 0.5f, 15f, 10f, x);
                    //ç≈ëÂ-ç≈è¨É_ÉÅÅ[ÉW = 70
                    DamageControl = LinearFunctionValue.GetValue(0f, 0.5f, param.SwordDamage4, param.SwordDamage4 - 70, x);
                    IsSuccess = false;
                    Zone.transform.localScale = new Vector3(ZoneScale, ZoneScale, ZoneScale);
                    StartCoroutine(ZoneAttack());
                }
            }
        }
    }

    IEnumerator ZoneAttack()
    {
        ZoneIsOn = !ZoneIsOn;
        Instantiate(Zone, PlayerTransform.transform.position, PlayerTransform.transform.rotation);
        yield return new WaitForSeconds(1f);
        ZoneIsOn = !ZoneIsOn;
    }
}
