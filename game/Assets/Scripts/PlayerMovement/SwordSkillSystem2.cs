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

                //ç≈ëÂíºåaÉ”7, ç≈è¨íºåaÉ”4.55
                var x = Mathf.Abs(0.5f - Value);
                var ZoneScale = LinearFunctionValue.GetValue(0f, 0.5f, 10f, 5f, x);
                //ç≈ëÂ-ç≈è¨É_ÉÅÅ[ÉW = 50
                DamageControl = LinearFunctionValue.GetValue(0f, 0.5f, param.SwordDamage3, param.SwordDamage3 - 50f, x);
                Zone.transform.localScale = new Vector3(ZoneScale, ZoneScale, ZoneScale);
                StartCoroutine(ZoneAttack());
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
