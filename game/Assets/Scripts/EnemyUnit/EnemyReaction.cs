using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyReaction : MonoBehaviour
{
    follow Enemyfollow = new follow();
    enemyUnit Unit = new enemyUnit();
    DamageAndCostManager param;

    SwordSkillSystem1 swordSkillSystem1;
    SwordSkillSystem2 swordSkillSystem2;
    SwordSkillSystem3 swordSkillSystem3;
    ScoreManager ScoreUI;
    public GameObject FirstKatana;
    public GameObject SecondKatana;
    public GameObject thirdKatana;
    float Hp, MaxHp, DownSpeed;
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

        Debug.Log(Unit.firstEnemy.appear);
        /*ItemGacha1 = new Gacha("Slow", "Use", 1, 1, Exitems[0]);
        ItemGacha2 = new Gacha("Cure", "Use", 2, 1, Exitems[1]);
        ItemGacha3 = new Gacha("Bomb", "Use", 3, 1, Exitems[2]);
        ItemGacha4 = new Gacha("Heist", "Use", 4, 1, Exitems[3]);
        ItemGacha5 = new Gacha("ExpItem", "Exp", 1, 10, Exitems[4]);
        ItemGachaLists.Add(ItemGacha1);
        ItemGachaLists.Add(ItemGacha2);
        ItemGachaLists.Add(ItemGacha3);
        ItemGachaLists.Add(ItemGacha4);
        ItemGachaLists.Add(ItemGacha5);*/
        swordSkillSystem1 = GameObject.Find("SwordWave").GetComponent<SwordSkillSystem1>();
        swordSkillSystem2 = GameObject.Find("FirstZone").GetComponent<SwordSkillSystem2>();
        swordSkillSystem3 = GameObject.Find("SecondZone").GetComponent<SwordSkillSystem3>();
        Hp = UnitStatus(Enemyname).Hp;
        MaxHp = Hp;
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
            Instantiate(Exitems[0], gameObject.transform.position, Quaternion.identity);
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
            }
            if(other.gameObject.name == "Cube(Clone)")
            {
                Hp -= swordSkillSystem1.DamageControl;
                Debug.Log(swordSkillSystem1.DamageControl);
            }
            HpGage.SetActive(true);
            HitSound.PlayOneShot(HitSound.clip);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.name == "ZoneAttack1(Clone)" && ZoneInterval == true)
        {
            StartCoroutine(ZoneHit(swordSkillSystem2.DamageControl));
        }
        if(other.gameObject.name == "ZoneAttack2(Clone)" && ZoneInterval == true)
        {
            StartCoroutine(ZoneHit(swordSkillSystem3.DamageControl));
        }
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
