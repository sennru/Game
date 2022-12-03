using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
    bool costs;
    int minutes;
    Text Timer;
    public GameObject[] walls;
    EnergyManager energyManager;
    ItemChange itemChange;

    private void Start()
    {
        costs = true;
        minutes = 0;
        controller = GetComponent<CharacterController>();

        energyManager = GameObject.Find("EnergyManager").GetComponent<EnergyManager>();
        itemChange = GameObject.Find("ItemChangeManager").GetComponent<ItemChange>();
    }
    void Update()
    {
        if (!Input.GetMouseButton(2))
        {
            RotY();

        }
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
        float YMove = Input.GetAxis("Mouse X");
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
        if(collision.gameObject.tag == "JumpPad")
        {
            moveDirection.y = jumpSpeed * 2;
        }
        if (collision.gameObject.tag == "SuperJumpPad")
        {
            moveDirection.y = jumpSpeed * 4;
            if (!controller.isGrounded)
            {
                gravity /= 3;
            }
        }
    }

    IEnumerator JetPack()
    {
        costs = !costs;
        energyManager.Energy -= 50;
        yield return new WaitForSeconds(0.25f);
        costs = !costs;
    }
}
