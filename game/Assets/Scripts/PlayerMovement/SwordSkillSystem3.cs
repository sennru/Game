using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CaluculateExtention;

public class SwordSkillSystem3 : SwordSkill
{
    public GameObject Zone, PlayerTransform;
    public float DamageControl;
    public AudioSource Ready;
    public AudioClip[] IAISound;

    // Update is called once per frame
    public override void SwordSkill3()
    {
        if(SwordIsOn.activeSelf == true && katanaIsOn[3].activeSelf == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Time.timeScale = 0.3f;
                Ready.clip = IAISound[0];
                Ready.time = 0f;
                Ready.PlayOneShot(IAISound[0]);
                Ready.clip = IAISound[1];
                Ready.Play();
                Ready.loop = true;
            }
            if (Input.GetMouseButtonUp(0))
            {
                Ready.loop = false;
                Time.timeScale = 1.0f;
                Ready.clip = IAISound[2];
                Ready.Play();
                Value = SkillSlider.value;
                energyManager.Energy -= param.CostSword4;

                //ç≈ëÂíºåaÉ”15, ç≈è¨íºåaÉ”10
                var x = Mathf.Abs(0.5f - Value);
                var ZoneScale = LinearFunctionValue.GetValue(0f, 0.5f, 15f, 10f, x);
                //ç≈ëÂ-ç≈è¨É_ÉÅÅ[ÉW = 70
                DamageControl = Mathf.Floor(LinearFunctionValue.GetValue(0f, 0.5f, param.SwordDamage4, param.SwordDamage4 - 70, x));
                Zone.transform.localScale = new Vector3(ZoneScale, ZoneScale, ZoneScale);
                Instantiate(Zone, PlayerTransform.transform.position, PlayerTransform.transform.rotation);
            }
        }
    }
}
