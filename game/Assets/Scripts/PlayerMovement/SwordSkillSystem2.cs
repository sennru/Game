using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CaluculateExtention;

public class SwordSkillSystem2 : SwordSkill
{
    public GameObject Zone, PlayerTransform;
    public AudioSource Ready;
    public AudioClip[] IAISound;
    public float DamageControl;

    const float LOWER_LIMIT = 50f;

    public override void SwordSkill2()
    {
        if(SwordIsOn.activeSelf == true && katanaIsOn[2].activeSelf == true)
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
                energyManager.Energy -= param.state.property.swordCost[1];
                Ready.clip = IAISound[1];
                Ready.time = 0.3f;
                Ready.Play();

                //ç≈ëÂíºåaÉ”7, ç≈è¨íºåaÉ”4.55
                var x = Mathf.Abs(0.5f - Value);
                var ZoneScale = LinearFunctionValue.GetValue(0f, 0.5f, 10f, 5f, x);
                //ç≈ëÂ-ç≈è¨É_ÉÅÅ[ÉW = 50
                DamageControl = Mathf.Floor(LinearFunctionValue.GetValue(0f, 0.5f, param.state.property.swordDamage[2], param.state.property.swordDamage[2] - LOWER_LIMIT, x));
                Zone.transform.localScale = new Vector3(ZoneScale, ZoneScale, ZoneScale);
                Instantiate(Zone, PlayerTransform.transform.position, PlayerTransform.transform.rotation);
            }
        }
        
    }
}
