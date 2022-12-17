using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwordSkill : MonoBehaviour
{
    [SerializeField]
    GameObject SwordSkillObject;
    [SerializeField]
    public GameObject[] katanaIsOn;
    public GameObject SwordIsOn;
    Slider SkillSlider;
    Animator SliderMovement;
    public float SliderValue;
    float Speed;
    public bool IsSuccess;

    public void Start()
    {
        SkillSlider = SwordSkillObject.GetComponent<Slider>();
        SliderMovement = SwordSkillObject.GetComponent<Animator>();
        SwordSkillObject.SetActive(false);
    }


    private void Update()
    {
        SwordSkillDefinition(0.3f);
    }
    public void SwordSkillDefinition(float AttackControl)
    {
        if (Input.GetMouseButtonDown(0) && katanaIsOn[0].activeSelf == false && SwordIsOn.activeSelf == true)
        {
            SwordSkillObject.SetActive(true);
            if(katanaIsOn[2].activeSelf == true)//‹Z‘I‘ð‚ªƒŒƒxƒ‹‚R‚ÌŽž
            {
                Speed = 1.4f;
                SliderMovement.SetFloat("Speed", Speed);
            }
            else if(katanaIsOn[3].activeSelf == true)
            {
                Speed = 1.6f * (1f / 0.3f); //ŽžŠÔŒ¸‘¬•ª‚ÌæŽZ
                SliderMovement.SetFloat("Speed", Speed);
            }
        }
        if (Input.GetMouseButtonUp(0) && katanaIsOn[0].activeSelf == false && SwordIsOn.activeSelf == true)
        {
            SliderValue = SkillSlider.value;
            if (SliderValue >= 0.5f - (AttackControl / 2f) && SliderValue <= 0.5f + (AttackControl / 2f))
            {
                IsSuccess = true;
            }
            else
            {
                IsSuccess = false;
            }
            SwordSkillObject.SetActive(false);
        }
    }
}
