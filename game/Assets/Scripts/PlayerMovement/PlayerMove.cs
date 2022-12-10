using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    //実質の速度
    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public float Sensitivity = 1f;
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
    bool costs, UseJetPack;
    public bool[] ItemUsed = new bool[4];
    public int[] ItemCount = new int[4];
    public GameObject[] walls;
    EnergyManager energyManager;
    ItemChange itemChange;
    DamageAndCostManager DamageManager;


    private void Start()
    {
        costs = true;
        controller = GetComponent<CharacterController>();

        DamageManager = GameObject.Find("ParamatorManager").GetComponent<DamageAndCostManager>();
        energyManager = GameObject.Find("EnergyManager").GetComponent<EnergyManager>();
        itemChange = GameObject.Find("ItemChangeManager").GetComponent<ItemChange>();
    }
    void Update()
    {
        if (!Input.GetMouseButton(2))
        {
            RotY();
        }
        if (itemChange.ItemOn[1] && Input.GetMouseButtonDown(1) && ItemUsed[0] == false && ItemCount[0] > 0)
        {
            StartCoroutine(Heist());
        }
        if (itemChange.ItemOn[2] && Input.GetMouseButtonDown(1) && ItemUsed[1] == false && ItemCount[1] > 0)
        {
            StartCoroutine(Strength());
        }
        if (itemChange.ItemOn[3] && Input.GetMouseButtonDown(1) && ItemUsed[2] == false && ItemCount[2] > 0)
        {
            StartCoroutine(Slow());
        }
        if (itemChange.ItemOn[4] && Input.GetMouseButtonDown(1) && ItemUsed[3] == false && ItemCount[3] > 0)
        {
            UseJetPack = true;
        }
        else
        {
            UseJetPack = false;
        }
        Debug.Log(ItemCount[0]);
        /*Vector3 rayPosition = transform.position + new Vector3(0.0f, 0.0f, 0.0f);
        Ray ray = new Ray(rayPosition, Vector3.down);
        bool isGround = Physics.Raycast(ray, distance);
        Debug.DrawRay(rayPosition, Vector3.down * distance, Color.red);*/
    }

    private void FixedUpdate()
    {
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            gravity = 20.0f;
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }
        moveDirection.y = moveDirection.y - (gravity * Time.deltaTime);
        if (!controller.isGrounded)
        {
            moveDirection.x = Input.GetAxis("Horizontal") * speed;
            moveDirection.z = Input.GetAxis("Vertical") * speed;
            moveDirection = transform.TransformDirection(moveDirection);
        }

        if (Input.GetKey("space") && energyManager.Energy >= 50f && itemChange.ItemOn[1] == true && transform.position.y < 120f)
        {
            moveDirection.y = jumpSpeed;
            if (costs == true)
            {
                StartCoroutine(JetPack());
            }
        }
        controller.Move(moveDirection * Time.deltaTime);
    }

    //左右の振り向き
    void RotY()
    {
        float YMove = Input.GetAxis("Mouse X") * 2 * Sensitivity;
        Vector3 YRot = transform.localEulerAngles;
        YRot.y += YMove;
        transform.localEulerAngles = YRot;
    }

    //フレームレート数(速度調整のため)
    float fps()
    {
        var FPS = 1f / Time.deltaTime;
        return FPS;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "MoveFloor")
        {
            transform.SetParent(collision.transform);
        }

        if(collision.gameObject.tag == "JumpPad")
        {
            moveDirection.y = jumpSpeed * 2;
        }
        if (collision.gameObject.tag == "SuperJumpPad")
        {
            moveDirection.y = jumpSpeed * 6.5f;
        }

        if(collision.gameObject.tag == "BoundGimmick")
        {
            moveDirection.x = 30f;
        }

        if(collision.gameObject.tag == "Item")
        {
            if(collision.gameObject.name == "Heist(Clone)")
            {
                ItemCount[0] += 1;
            }
            if (collision.gameObject.name == "Strength(Clone)")
            {
                ItemCount[1] += 1;
            }
            if (collision.gameObject.name == "Slow(Clone)")
            {
                ItemCount[2] += 1;
            }
            if(collision.gameObject.name == "JetPack(Clone)")
            {
                ItemCount[3] += 1;
            }
            Destroy(collision.gameObject);
            
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "MoveFloor")
        {
            transform.SetParent(null);
        }
    }

    IEnumerator JetPack()
    {
        costs = !costs;
        energyManager.Energy -= 50;
        yield return new WaitForSeconds(0.25f);
        costs = !costs;
    }

    IEnumerator Heist()
    {
        ItemUsed[0] = !ItemUsed[0];
        speed *= 20.0f;
        ItemCount[0] -= 1;
        Debug.Log("Use Heist");
        yield return new WaitForSeconds(30);
        speed /= 20f;
        ItemUsed[0] = !ItemUsed[0];
    }

    IEnumerator Strength()
    {
        ItemUsed[1] = !ItemUsed[1];
        DamageManager.DamageBuff(2f);
        Debug.Log("Use Strength");
        ItemCount[1] -= 1;
        yield return new WaitForSeconds(30);
        DamageManager.DamageBuff(1 / 2f);
        ItemUsed[1] = !ItemUsed[1];
    }

    IEnumerator Slow()
    {
        Debug.Log("Use Slow");
        ItemUsed[2] = !ItemUsed[2];
        ItemCount[2] -= 1;
        yield return new WaitForSeconds(30);
        ItemUsed[2] = !ItemUsed[2];
    }
}

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
    public GameObject HpUI;
    public GameObject DamageCollider;
    public GameObject DamageColliderPosition;
    public GameObject Sword;
    public GameObject Gun;
    public GameObject IsOnKatana;
    enemyUnit enemy = new enemyUnit();
    [System.NonSerialized]
    public int Lv, Exp;

    bool InvincibleTime = true;
    public float GunDamage = 20f;
    public float SwordDamage = 100f;

    private void Start()
    {
        Lv = 1;
        DamageSound = this.GetComponent<AudioSource>();
        HpUI = GameObject.Find("HpUI");
        for(int i = 0; i < HpImages.Length; i++)
        {
            HpImages[i] = HpUI.transform.GetChild(i).gameObject;
        }
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
        if (InvincibleTime == true)
        {
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
}
