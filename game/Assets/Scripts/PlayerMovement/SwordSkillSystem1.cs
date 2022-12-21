using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CaluculateExtention;

public class SwordSkillSystem1 : SwordSkill
{
    public GameObject ImpactObject;
    public GameObject Camera;
    public AudioSource Ready;
    public AudioClip[] IAISound;
    float CubeSpeed;
    public float DamageControl;

    public override void SwordSkill1()
    {
        if (SwordIsOn.activeSelf == true && katanaIsOn[1].activeSelf == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ready.clip = IAISound[0];
                Ready.time = 0f;
                Ready.PlayOneShot(IAISound[0]);
            }
            if (Input.GetMouseButtonUp(0))
            {
                Value = SkillSlider.value;
                Ready.clip = IAISound[1];
                Ready.time = 0.5f;
                Ready.Play();
                var x = Mathf.Abs(0.5f - Value);
                CubeSpeed = LinearFunctionValue.GetValue(0f, 0.5f, 80f, 30f, x);
                DamageControl = Mathf.Floor(LinearFunctionValue.GetValue(0f, 0.5f, param.SwordDamage2, param.SwordDamage2 - 50f, x));
                SwordSkillDefinition(CubeSpeed);
            }
        }
    }

    public void SwordSkillDefinition(float Speed)
    {
        GameObject impact = (GameObject)Instantiate(ImpactObject, transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0));
        Rigidbody impactRb = impact.GetComponent<Rigidbody>();
        impactRb.velocity = Camera.transform.forward * Speed; //カメラの向いてる方向へブロックを発射
        energyManager.Energy -= param.CostSword2;
    }
}
