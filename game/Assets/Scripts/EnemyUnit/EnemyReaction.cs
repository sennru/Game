using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyReaction : MonoBehaviour
{
    follow Enemyfollow = new follow();
    enemyUnit Unit = new enemyUnit();
    SwordSkill swordSkill;
    DamageAndCostManager param;
    SwordSkillSystem2 swordSkillSystem2;
    SwordSkillSystem3 swordSkillSystem3;
    ScoreManager ScoreUI;
    public GameObject SecondKatana;
    public GameObject thirdKatana;
    float Hp;
    float MaxHp;
    float DownSpeed;
    public int score;
    public Slider slider;
    public GameObject HpGage;
    AudioSource HitSound;
    public GameObject[] Exitems;
    public Gacha ItemGacha1;
    public Gacha ItemGacha2;
    public Gacha ItemGacha3;
    public Gacha ItemGacha4;
    public Gacha ItemGacha5;
    public List<Gacha> ItemGachaLists;
    GachaSystem gachaSystem = new GachaSystem();
    bool ZoneInterval = true;
    PlayerMove PlayerSlow;

    [SerializeField]
    private string Enemyname;

    void Start()
    {
        ItemGacha1 = new Gacha("Slow", "Use", 1, 1, Exitems[0]);
        ItemGacha2 = new Gacha("Cure", "Use", 2, 1, Exitems[1]);
        ItemGacha3 = new Gacha("Bomb", "Use", 3, 1, Exitems[2]);
        ItemGacha4 = new Gacha("Heist", "Use", 4, 1, Exitems[3]);
        ItemGacha5 = new Gacha("ExpItem", "Exp", 1, 10, Exitems[4]);
        /*ItemGachaLists.Add(ItemGacha1);
        ItemGachaLists.Add(ItemGacha2);
        ItemGachaLists.Add(ItemGacha3);
        ItemGachaLists.Add(ItemGacha4);
        ItemGachaLists.Add(ItemGacha5);*/
        swordSkillSystem2 = SecondKatana.GetComponent<SwordSkillSystem2>();
        swordSkillSystem3 = thirdKatana.GetComponent<SwordSkillSystem3>();
        Hp = UnitStatus(Enemyname).Hp;
        //Damage = Unit.firstEnemy.Damage;
        MaxHp = Hp;
        swordSkill = GameObject.Find("SwordSkillManager").GetComponent<SwordSkill>();
        slider.value = 1f;
        ScoreUI = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        HitSound = GameObject.Find("Animation").GetComponent<AudioSource>();
        param = GameObject.Find("ParamatorManager").GetComponent<DamageAndCostManager>();
        PlayerSlow = GameObject.Find("Player").GetComponent<PlayerMove>();
    }

    void Update()
    {
        Enemyfollow.enemyMovement(UnitStatus(Enemyname).speed * DownSpeed, this.gameObject);//ƒRƒR‚ð‹¤’Ê
        slider.value = Hp / MaxHp;
        if (Hp <= 0)
        {
            //Instantiate(gachaSystem.ItemGacha(ItemGachaList).ItemObject, gameObject.transform.position, Quaternion.identity);
            Instantiate(Exitems[4], gameObject.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            ScoreUI.Score += UnitStatus(Enemyname).getScore;
        }
        if(PlayerSlow.ItemUsed[2] == true)
        {
            DownSpeed = 0.7f;
        }
        else
        {
            DownSpeed = 1.0f;
        }
    }
    Enemyinfo UnitStatus(string EnemyName)
    {
        if(EnemyName == "first")
        {
            return Unit.firstEnemy;
        }
        else if (EnemyName == "second")
        {
            return Unit.secondEnemy;
        }
        else if (EnemyName == "third")
        {
            return Unit.thirdEnemy;
        }
        else if (EnemyName == "last")
        {
            return Unit.lastEnemy;
        }
        else if (EnemyName == "boss")
        {
            return Unit.boss;
        }
        return Unit.firstEnemy;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BulletAttack")
        {
            if(other.gameObject.name == "bullet(Clone)")
            {
                Hp -= param.GunDamage1;
            }

            if(other.gameObject.name == "bullet2(Clone)")
            {
                Hp -= param.GunDamage2;
            }
            if (other.gameObject.name == "Explosion(Clone)")
            {
                Hp -= param.GunDamage3;
            }
            HpGage.SetActive(true);
            HitSound.PlayOneShot(HitSound.clip);
        }
        if(other.gameObject.tag == "SwordAttack")
        {
            if (other.gameObject.name == "DamageCollider(Clone)")
            {
                Hp -= param.SwordDamage1;
                Debug.Log(2);
            }
            if(other.gameObject.name == "Cube(Clone)")
            {
                if(swordSkill.IsSuccess == true)
                {
                    Hp -= param.SwordDamage2;
                    Debug.Log("300Damage");
                }
                else if(swordSkill.IsSuccess == false)
                {
                    Hp -= param.SwordDamage2 / 3;
                    Debug.Log("100Damage");
                }
            }
            HpGage.SetActive(true);
            HitSound.PlayOneShot(HitSound.clip);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.name == "ZoneAttack1(Clone)" && ZoneInterval == true)
        {
            if(swordSkillSystem2.IsSuccess == true)
            {
                StartCoroutine(ZoneHit(param.SwordDamage3));
            }
            else
            {
                StartCoroutine(ZoneHit(swordSkillSystem2.DamageControl));
            }
        }
        if(other.gameObject.name == "ZoneAttack2(Clone)" && ZoneInterval == true)
        {
            if (swordSkillSystem3.IsSuccess == true)
            {
                StartCoroutine(ZoneHit(param.SwordDamage4));
            }
            else
            {
                StartCoroutine(ZoneHit(swordSkillSystem3.DamageControl));
            }        }
    }

    IEnumerator ZoneHit(float Damage)
    {
        ZoneInterval = false;
        Hp -= Damage;
        HitSound.PlayOneShot(HitSound.clip);
        yield return new WaitForSeconds(1f / 10f);
        ZoneInterval = true;
    }

}
