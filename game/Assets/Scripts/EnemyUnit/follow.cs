using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follow : MonoBehaviour
{
    Vector3 myTransform;
    Vector3 playerTransform;
    Vector3 velocityVector;
    float theta, phi;
    float sqrt;
    float VectorWeight;
    // 敵の移動とその速度
    public void enemyMovement(float moveVelocity, GameObject enemy)
    {
        GameObject player = GameObject.Find("Player");
        //速度ベクトルの計算
        playerTransform = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        myTransform = new Vector3(enemy.transform.position.x, enemy.transform.position.y, enemy.transform.position.z);
        VectorWeight = Mathf.Sqrt(Mathf.Pow(playerTransform.x - myTransform.x, 2) + Mathf.Pow(playerTransform.y - myTransform.y, 2) + Mathf.Pow(playerTransform.z - myTransform.z, 2));
        //x,y座標の2乗の和の平方根
        sqrt = Mathf.Sqrt(Mathf.Pow((playerTransform.x - myTransform.x), 2) + Mathf.Pow((playerTransform.y - myTransform.y), 2));
        //動きを一定速度に
        velocityVector = (moveVelocity / VectorWeight) * (playerTransform - myTransform);

        enemy.transform.rotation = Camera.main.transform.rotation;
        //体の向きの決定
        /*
        if (VectorWeight > 0)
        {
            theta = -Mathf.Acos((playerTransform.z - myTransform.z) / VectorWeight) * Mathf.Rad2Deg;
        }

        if (velocityVector.y >= 0 && sqrt != 0)
        {
            phi = Mathf.Acos((playerTransform.x - myTransform.x) / sqrt) * Mathf.Rad2Deg;
        }
        else if (velocityVector.y < 0 && sqrt != 0)
        {
            phi = -Mathf.Acos((playerTransform.x - myTransform.x) / sqrt) * Mathf.Rad2Deg;
        }
        else if (sqrt == 0)
        {
            phi = 0f;
        }
        //回転角を指定
        enemy.transform.rotation = Quaternion.Euler(0f, theta, phi);*/

        Rigidbody rb = enemy.transform.GetComponent<Rigidbody>();
        rb.velocity = velocityVector;
        //Debug.Log(phi);
        //Debug.Log(theta);
    }

}
