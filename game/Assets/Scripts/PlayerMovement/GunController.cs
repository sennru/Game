using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunController : MonoBehaviour
{
    DamageAndCostManager param;
    public GameObject bulletPrefab;
    AudioSource GunSound;
    public float shotSpeed;
    public int bullets, MaxBullets;
    float ShotInterval;
    bool delay = false;
    Text BulletsAmount;
    public AudioClip ShotSound;
    public AudioClip ReloadSound;
    public AudioClip BulletsEmpty;
    public GameObject[] Bullets;

    EnergyManager energyManager;
    PlayerUnit playerUnit;

    private void Start()
    {
        param = GameObject.Find("ParamatorManager").GetComponent<DamageAndCostManager>();
        playerUnit = GameObject.Find("Player").GetComponent<PlayerUnit>();
        GunSound = this.GetComponent<AudioSource>();
        energyManager = GameObject.Find("EnergyManager").GetComponent<EnergyManager>();
        BulletsAmount = GameObject.Find("Bullets").GetComponent<Text>();
        MaxBullets = 24;
        bullets = MaxBullets;
    }
    void Update()
    {
        MaxBulletsControll();
        if (Input.GetKey(KeyCode.Mouse0) && delay == false && bullets > 0 && bulletPrefab == Bullets[0])
        {
            StartCoroutine(GunShot(bulletPrefab));
        }
        else if (Input.GetKey(KeyCode.Mouse0) && delay == false && bullets > 2 && bulletPrefab == Bullets[1])
        {
            StartCoroutine(GunShot(bulletPrefab));
        }
        else if (Input.GetKey(KeyCode.Mouse0) && delay == false && bullets == MaxBullets && bulletPrefab == Bullets[2])
        {
            StartCoroutine(GunShot(bulletPrefab));
        }
        Reload();
    }

    IEnumerator GunShot(GameObject HowBullet)
    {
        if(HowBullet == Bullets[0] || (HowBullet == Bullets[1] && energyManager.Energy >= param.CostGun2) || (HowBullet == Bullets[2] && energyManager.Energy >= param.CostGun3))
        {
            delay = true;
            GameObject bullet = (GameObject)Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.parent.eulerAngles.x - 90, transform.parent.eulerAngles.y, 0));
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            bulletRb.AddForce(transform.forward * shotSpeed);
            GunSound.clip = ShotSound;
            GunSound.PlayOneShot(ShotSound);
            if (HowBullet == Bullets[0])
            {
                bullets -= 1;
                yield return new WaitForSeconds(ShotInterval);
            }
            else if (HowBullet == Bullets[1])
            {
                bullets -= 3;
                energyManager.Energy -= param.CostGun2;
                yield return new WaitForSeconds(ShotInterval * 2.33f);
            }
            else if (HowBullet == Bullets[2])
            {
                bullets -= MaxBullets;
                energyManager.Energy -= param.CostGun3;
                yield return new WaitForSeconds(1);
            }

            delay = false;
        }
    }

    void Reload()
    {

        if (Input.GetKeyDown("r") && energyManager.Energy >= param.CostGun1)
        {
            GunSound.clip = ReloadSound;
            GunSound.PlayOneShot(ReloadSound);
            for (int i = 1; i <= MaxBullets; i++)
            {
                if (energyManager.Energy < param.CostGun1 || bullets >= MaxBullets)
                {
                    break;
                }
                bullets++;
                energyManager.Energy -= param.CostGun1;
            }
        }
        else if (Input.GetKeyDown("r") && energyManager.Energy < param.CostGun1)
        {
            GunSound.clip = BulletsEmpty;
            GunSound.PlayOneShot(BulletsEmpty);
        }
        BulletsAmount.text = "Bullets : " + bullets.ToString();
    }

    void MaxBulletsControll()
    {
        if(playerUnit.Lv == 1)
        {
            MaxBullets = 24;
            ShotInterval = 1f / 8f;
        }
        else if (playerUnit.Lv == 2)
        {
            MaxBullets = 36;
            ShotInterval = 1f / 12f;
        }
        else if (playerUnit.Lv == 3)
        {
            MaxBullets = 48;
            ShotInterval = 1f / 16f;
        }

    }
}
