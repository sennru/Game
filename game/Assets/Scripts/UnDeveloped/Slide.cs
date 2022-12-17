using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CaluculateExtention;

public class Slide : MonoBehaviour
{
    public Vector3 PurposePosition;
    Vector3 MyPos;
    float Seconds;
    float Seconds2;

    [SerializeField]
    float speed = 0f;

    public float RoundTime;
    public float SpeedChangeTime;
    // Start is called before the first frame update
    private void Awake()
    {
        MyPos = transform.localPosition;

        PurposePosition += MyPos;
    }

    // Update is called once per frame
    void Update()
    {
        var Weight = PositionCalculate.Distance(PurposePosition, MyPos);
        var VelVector = (PurposePosition - MyPos) / Weight;
        transform.localPosition += SpeedControl(Weight, RoundTime, SpeedChangeTime) * VelVector * Time.deltaTime;
        Debug.Log(VelVector);

        Seconds2 += Time.deltaTime;
        if(Seconds2 >= RoundTime)
        {
            Seconds2 = 0f;
            transform.localPosition = MyPos;
        }
    }

    float SpeedControl(float Dis, float RT, float SpeedChange)
    {
        Seconds += Time.deltaTime;
        var Vmax = Dis / (RT - 2 * SpeedChange);
        var alpha = Vmax / SpeedChange * 2f;
        if(RT >= SpeedChange * 4)
        {
            if (Seconds < SpeedChange)
            {
                speed += alpha * Time.deltaTime;
            }
            else if (Seconds >= (RT / 2) - SpeedChange && Seconds < RT / 2 + SpeedChange)
            {
                speed -= alpha * Time.deltaTime;
            }
            else if (Seconds >= RT - SpeedChange && Seconds < RT)
            {
                speed += alpha * Time.deltaTime;
            }
            else if (Seconds >= RT)
            {
                Seconds = 0f;
                speed = 0f;
            }
        }

        return speed;
    }
}
