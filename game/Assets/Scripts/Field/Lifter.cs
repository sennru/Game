using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifter : MonoBehaviour
{
    public int Phase = 0;
    Vector3[] TurnVec = new Vector3[5];
    public float LiftSpeed;
    Vector3 pos = Vector3.zero;
    Vector3 MyPos;
    void Start()
    {
        TurnVec[0] = new Vector3(30f, 0, 0);
        TurnVec[1] = new Vector3(30f, 0, 50f);
        TurnVec[2] = new Vector3(-30f, 0, 50f);
        TurnVec[3] = new Vector3(-30f, 0, 0);
        TurnVec[4] = new Vector3(0f, 20f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        MyPos = transform.localPosition;
        TurnPoints(TurnVec);
        transform.localPosition += pos * Time.deltaTime * LiftSpeed;
        Debug.Log(pos * LiftSpeed);
    }

    void TurnPoints(Vector3[] TurnPos)
    {
        for (int i = 0; i < TurnPos.Length; i++)
        {
            if (Distance(TurnPos[i], MyPos) < 0.1f && Phase == i)
            {
                Phase += 1;
                transform.localPosition = TurnPos[i];
            }
            if (Phase == TurnPos.Length)
            {
                pos = Vector3.zero;
            }
            else if(i == Phase)
            {
                var Weight = Mathf.Sqrt(Mathf.Pow(TurnPos[i].x - MyPos.x, 2) + Mathf.Pow(TurnPos[i].y - MyPos.y, 2) + Mathf.Pow(TurnPos[i].z - MyPos.z, 2));
                pos = (TurnPos[i] - MyPos) / Weight;
            }
        }
    }

    float Distance(Vector3 a, Vector3 b)
    {
        var Dis = Mathf.Sqrt(Mathf.Pow(a.x - b.x, 2) + Mathf.Pow(a.y - b.y, 2) + Mathf.Pow(a.z - b.z, 2));
        return Dis;
    }

    public void ResetPosition()
    {
        Phase = 0;
        transform.localPosition = Vector3.zero;
    }
}
