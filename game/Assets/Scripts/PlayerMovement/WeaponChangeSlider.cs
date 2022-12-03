using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponChangeSlider : MonoBehaviour
{
    Slider ChangeSlider;
    GameObject WeaponChange;
    GunController Gun;
    public GameObject NowWeaponUI; 
    public GameObject SwordIsOn, GunIsOn;
    public GameObject[] katanas;
    public GameObject[] Bullets;
    bool IsMove = false;
    string NowWeapon;
    // Start is called before the first frame update
    void Start()
    {
        Gun = GameObject.Find("Shooting").GetComponent<GunController>();
        WeaponChange = transform.GetChild(0).gameObject;
        ChangeSlider = WeaponChange.GetComponent<Slider>();
        ChangeSlider.value = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            ChangeSlider.value = 0f;
            KatanaOnSystem(0);
        }
        WeaponChangeSystem();
        if (IsMove == true)
        {
            ChangeSlider.value += Input.GetAxis("Mouse X") * 0.03f;
        }

    }

    void WeaponChangeSystem()
    {
        if (Input.GetMouseButtonDown(2))
        {
            IsMove = true;
            WeaponChange.SetActive(IsMove);
            NowWeaponUI.SetActive(true);
        }
        if(SwordIsOn.activeSelf == true)
        {
            if (ChangeSlider.value > 3f / 4f)
            {
                if (Input.GetMouseButtonUp(2))
                {
                    KatanaOnSystem(3);
                    IsMove = false;
                    WeaponChange.SetActive(IsMove);
                    NowWeaponUI.SetActive(false);
                }
                NowWeapon = "Sword4";
            }
            else if (ChangeSlider.value > 2f / 4f && ChangeSlider.value <= 3f / 4f)
            {
                if (Input.GetMouseButtonUp(2))
                {
                    KatanaOnSystem(2);
                    IsMove = false;
                    WeaponChange.SetActive(IsMove);
                    NowWeaponUI.SetActive(false);
                }
                NowWeapon = "Sword3";
            }
            else if (ChangeSlider.value > 1f / 4f && ChangeSlider.value <= 2f / 4f)
            {
                if (Input.GetMouseButtonUp(2))
                {
                    KatanaOnSystem(1);
                    IsMove = false;
                    WeaponChange.SetActive(IsMove);
                    NowWeaponUI.SetActive(false);
                }
                NowWeapon = "Sword2";
            }
            else if (ChangeSlider.value <= 1f / 4f)
            {
                if (Input.GetMouseButtonUp(2)) 
                {
                    KatanaOnSystem(0);
                    IsMove = false;
                    WeaponChange.SetActive(IsMove);
                    NowWeaponUI.SetActive(false);
                }
                NowWeapon = "Sword1";
            }
        }

        if(GunIsOn.activeSelf == true)
        {
            if (ChangeSlider.value > 2f / 3f)
            {
                if(Input.GetMouseButtonUp(2))
                {
                    Gun.bulletPrefab = Bullets[2];
                    IsMove = false;
                    WeaponChange.SetActive(IsMove);

                    NowWeaponUI.SetActive(false);
                }
                NowWeapon = "Gun3";
            }
            else if (ChangeSlider.value > 1f / 3f && ChangeSlider.value <= 2f / 3f)
            {
                if (Input.GetMouseButtonUp(2))
                {
                    Gun.bulletPrefab = Bullets[1];
                    IsMove = false;
                    WeaponChange.SetActive(IsMove);
                    NowWeaponUI.SetActive(false);
                }
                NowWeapon = "Gun2";
            }
            else if (ChangeSlider.value <= 1f / 3f)
            {
                if (Input.GetMouseButtonUp(2)) 
                {
                    Gun.bulletPrefab = Bullets[0];
                    IsMove = false;
                    WeaponChange.SetActive(IsMove);
                    NowWeaponUI.SetActive(false);
                }
                NowWeapon = "Gun1";
            }
        }
        NowWeaponUI.GetComponent<Text>().text = NowWeapon;
    }

    void KatanaOnSystem(int i)
    {
        for(int j = 0; j < 4; j++)
        {
            if(j == i)
            {
                katanas[j].SetActive(true);
            }
            else
            {
                katanas[j].SetActive(false);
            }
        }
    }
}
