using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUnit : MonoBehaviour
{
    public float Hp = 12f;
    string mode = "Gun";
    bool swing = true;
    public Animator anim;
    public Animator damageReactionUI;
    AudioSource DamageSound;
    public AudioSource SwingSound;
    public GameObject[] HpImages;
    public GameObject DamageCollider;
    public GameObject DamageColliderPosition;
    public GameObject Sword;
    public GameObject Gun;
    public GameObject IsOnKatana;
    enemyUnit enemy = new enemyUnit();
    public Text ScoreToExp;
    [System.NonSerialized]
    public int Lv, Exp;

    bool InvincibleTime = true;
    public float GunDamage = 20f;
    public float SwordDamage = 100f;

    private void Start()
    {
        Lv = 1;
        DamageSound = this.GetComponent<AudioSource>();
    }

    private void Update()
    {
        LevelSystem();
        playerMode(mode);
        for (int i = 0; i < 12; i++)
        {
            if (i < Hp)
            {
                HpImages[i].SetActive(true);
            }
            else if (i >= Hp)
            {
                HpImages[i].SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(InvincibleTime == true){
            StartCoroutine(Invincible(other));
        }
    }

    //eを押すとモードをかえる
    void playerMode(string modeString)
    {
        if (modeString == "Gun")
        {
            if (Input.GetKeyDown("e"))
            {
                mode = "Sword";
                Sword.SetActive(true);
                Gun.SetActive(false);
                anim.Play("Gun", 0, 0.0f);
            }
        }
        if (modeString == "Sword")
        {
            if (Input.GetKeyDown("e"))
            {
                mode = "Gun";
                Sword.SetActive(false);
                Gun.SetActive(true);
                anim.Play("Gun", 0, 0.0f);
            }
            if (Input.GetMouseButtonDown(0) && IsOnKatana.activeSelf == true)
            {
                if (swing == true)
                {
                    StartCoroutine(SwordMotion());
                }
            }
        }
    }
    IEnumerator SwordMotion()
    {
        bool mode = anim.GetBool("Mode");

        swing = false;
        mode = true;
        anim.SetBool("Mode", mode);
        SwingSound.Play();
        Instantiate(DamageCollider, DamageColliderPosition.transform.position, DamageColliderPosition.transform.rotation);
        yield return new WaitForSeconds(0.5f);

        mode = false;
        anim.SetBool("Mode", mode);
        swing = true;
    }

    IEnumerator IAIMotion()
    {

        yield return 0;
    }

    IEnumerator Invincible(Collider enemyObject)
    {
        if (enemyObject.tag == "Enemy")
        {
            InvincibleTime = !InvincibleTime;
            if (enemyObject.name == "Enemy1(Clone)")
            {
                Hp -= enemy.firstEnemy.Damage;
            }
            if (enemyObject.name == "Enemy2(Clone)")
            {
                Hp -= enemy.secondEnemy.Damage;
            }
            if (enemyObject.name == "Enemy3(Clone)")
            {
                Hp -= enemy.thirdEnemy.Damage;
            }
            if (enemyObject.name == "Enemy4(Clone)")
            {
                Hp -= enemy.lastEnemy.Damage;
            }
            if (enemyObject.name == "Boss(Clone)")
            {
                Hp -= enemy.boss.Damage;
            }
            damageReactionUI.Play("DamageShake", 0, 0.0f);
            DamageSound.Play();

            yield return new WaitForSeconds(2);
            InvincibleTime = !InvincibleTime;
        }
    }
    void LevelSystem()
    {
        
        if (Exp >= 500)
        {
            Lv = 3;
        }
        else if (Exp >= 200)
        {
            Lv = 2;
        }
        else
        {
            Lv = 1;
        }
    }
    void GameOver()
    {
        if (Hp <= 0)
        {

            //ゲームオーバーのUIを出す
        }
    }
}

    // Update is called once per frame
