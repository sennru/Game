using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    //ŽÀŽ¿‚Ì‘¬“x
    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public float Sensitivity = 1f;
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
    bool costs, UseJetPack, GetPower;
    [System.NonSerialized]
    public bool[] ItemUsed = new bool[3];
    [System.NonSerialized]
    public int[] ItemCount = new int[5];
    public GameObject[] walls;
    EnergyManager energyManager;
    ItemChange itemChange;
    PropertyManager DamageManager;


    private void Start()
    {
        costs = true;
        controller = GetComponent<CharacterController>();

        DamageManager = GameObject.Find("ParamatorManager").GetComponent<PropertyManager>();
        energyManager = GameObject.Find("EnergyManager").GetComponent<EnergyManager>();
        itemChange = GameObject.Find("ItemChangeManager").GetComponent<ItemChange>();

        for(int i = 0; i < ItemCount.Length; i++)
        {
            ItemCount[i] = 0;
        }
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
        UsingJetPack();
        Power();
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
        controller.Move(moveDirection * Time.deltaTime);
    }

    //¶‰E‚ÌU‚èŒü‚«
    void RotY()
    {
        float YMove = Input.GetAxis("Mouse X") * 2 * Sensitivity;
        Vector3 YRot = transform.localEulerAngles;
        YRot.y += YMove;
        transform.localEulerAngles = YRot;
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
            moveDirection.y = jumpSpeed * 6.6f;
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
            if(collision.gameObject.name == "Power")
            {
                ItemCount[4] += 1;
            }
            Destroy(collision.gameObject);
            
        }
        if(collision.gameObject.tag == "DamageBound")
        {
            moveDirection.y = jumpSpeed * 4f;

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
        speed *= 2f;
        ItemCount[0] -= 1;
        Debug.Log("Use Heist");
        yield return new WaitForSeconds(30);
        speed /= 2f;
        ItemUsed[0] = !ItemUsed[0];
    }

    IEnumerator Strength()
    {
        ItemUsed[1] = !ItemUsed[1];
        DamageManager.multiple *= 2f;
        Debug.Log("Use Strength");
        ItemCount[1] -= 1;
        yield return new WaitForSeconds(30);
        DamageManager.multiple *= 0.5f;
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

    void UsingJetPack()
    {
        if (itemChange.ItemOn[4] && ItemCount[3] > 0)
        {
            UseJetPack = true;
        }
        else
        {
            UseJetPack = false;
        }
        if (Input.GetKey("space") && energyManager.Energy >= 50f && UseJetPack == true && transform.position.y < 120f)
        {
            moveDirection.y = jumpSpeed;
            if (costs == true)
            {
                StartCoroutine(JetPack());
            }
        }
    }

    void Power()
    {
        if (ItemCount[4] > 0)
        {
            DamageManager.multiple *= 2f;
            ItemCount[4] -= 1;
        }
    }
}
