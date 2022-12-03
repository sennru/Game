using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CaluculateExtention;

public class wall : MonoBehaviour
{
    GameObject PlayerObject;
    public float appearLimitDistance = 15f;
    byte[] alpha = new byte[4];
    public GameObject[] Walls;
    GameObject WorldTimer;

    float seconds;
    float limitDistance;
    void Start()
    {
        WorldTimer = GameObject.Find("WorldTimer");
        PlayerObject = GameObject.Find("Player");
        for (int i = 0; i < Walls.Length; i++)
        {
            alpha[i] = 0;
            Walls[i].GetComponent<Renderer>().material.color = new Color32(0, 0, 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        MoveWalls();
        TransLucent();
    }

    void MoveWalls()
    {
        seconds = WorldTimer.GetComponent<WorldTime>().WorldTimeSeconds;
        if (seconds <= 120f)
        {
            limitDistance = LinearFunctionValue.GetValue(0f, 120f, 30f, 100f, seconds);
        }
        Vector3[] pos = new Vector3[4];
        pos[0].x = limitDistance + 0.5f;
        pos[1].z = limitDistance + 0.5f;
        pos[2].x = -limitDistance - 0.5f;
        pos[3].z = -limitDistance - 0.5f;

        for (int i = 0; i < pos.Length; i++)
        {
            Walls[i].transform.position = pos[i];
        }
    }
    void TransLucent()
    {
        float[] distance = new float[4];

        distance[0] = Mathf.Abs(PlayerObject.transform.position.x - Walls[0].transform.position.x);
        distance[1] = Mathf.Abs(PlayerObject.transform.position.z - Walls[1].transform.position.z);
        distance[2] = Mathf.Abs(PlayerObject.transform.position.x - Walls[2].transform.position.x);
        distance[3] = Mathf.Abs(PlayerObject.transform.position.z - Walls[3].transform.position.z);

        for (int i = 0; i < distance.Length; i++)
        {
            if (distance[i] < appearLimitDistance)
            {
                alpha[i] = (byte)LinearFunctionValue.GetValue(0f, appearLimitDistance, 150f, 0f, distance[i]);
                Walls[i].GetComponent<Renderer>().material.color = new Color32(0, 0, 0, alpha[i]);
                //Debug.Log(distance[0]);
            }
            else
            {
                alpha[i] = 0;
                Walls[i].GetComponent<Renderer>().material.color = new Color32(0, 0, 0, 0);
            }
        }
    }
}
