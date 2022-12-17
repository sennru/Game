using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemChange : MonoBehaviour
{
    public GameObject circleUI;
    public Text SelectNow;
    public GameObject[] BackGroundUI;
    Vector3 GetFirstPosition;
    Vector3 MoveVec;
    public int Items;
    [System.NonSerialized]
    public bool[] ItemOn;
    // Start is called before the first frame update
    void Start()
    {
        ItemOn = new bool[Items];
        GetFirstPosition = circleUI.transform.position;
        ItemOn[0] = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("f"))
        {
            circleUI.SetActive(true);
            for(int i = 0; i < BackGroundUI.Length; i++)
            {
                BackGroundUI[i].SetActive(true);
            }
            circleUI.transform.position = GetFirstPosition;
            MoveVec = new Vector3(0f, 0f, 0f);
        }
        if (Input.GetKey("f"))
        {
            MoveVec += new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0f) * 10f;
            if(CircleMoveLimit(60f, MoveVec) == false)
            {
                circleUI.transform.position = GetFirstPosition + MoveVec;
            }
            else
            {
                MoveVec *= 0.98f;
            }
            switch(ItemSelect(MoveVec, Items))
            {
                case 0:
                    SelectNow.text = "None";
                    break;
                case 1:
                    SelectNow.text = "heist";
                    break;
                case 2:
                    SelectNow.text = "Strength";
                    break;
                case 3:
                    SelectNow.text = "Slow";
                    break;
                case 4:
                    SelectNow.text = "JetPack";
                    break;
            }
        }
        if (Input.GetKeyUp("f"))
        {
            UseItemSwitch(ItemSelect(MoveVec, Items));
            circleUI.SetActive(false);
            for (int i = 0; i < BackGroundUI.Length; i++)
            {
                BackGroundUI[i].SetActive(false);
            }
        }
    }

    bool CircleMoveLimit(float radius, Vector3 MoveVector)
    {
        var r = Mathf.Sqrt(Mathf.Pow(MoveVector.x, 2) + Mathf.Pow(MoveVector.y, 2));
        if(r > radius)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    float CurrentAngle(Vector3 MoveVector)
    {
        float Angle;
        Angle = Mathf.Atan(MoveVector.y / MoveVector.x);
        if(MoveVec.x < 0 && MoveVec.y > 0)
        {
            return Angle * Mathf.Rad2Deg + 180f;
        }
        else if(MoveVec.x < 0 && MoveVec.y < 0)
        {
            return Angle * Mathf.Rad2Deg + 180f;
        }
        else if (MoveVec.x > 0 && MoveVec.y < 0)
        {
            return Angle * Mathf.Rad2Deg + 360f;
        }
        else
        {
            return Angle * Mathf.Rad2Deg;
        }
    }

    int ItemSelect(Vector3 MoveVector, int ItemCount)
    {
        var Angle = CurrentAngle(MoveVector);
        var borderAngle = 360f / ItemCount;
        float[] border = new float[ItemCount];
        int number = 9999;
        for(int i = 0; i < ItemCount; i++)
        {
            border[i] = borderAngle * i;
        }

        for(int j = 0; j < ItemCount - 1; j++)
        {
            if(Angle < border[j + 1] && Angle > border[j])
            {
                number = j;
            }
            else
            {
                continue;
            }
        }

        if(number == 9999)
        {
            number = ItemCount - 1;
        }

        return number;
    }

    void UseItemSwitch(int i)
    {
        for (int j = 0; j < Items; j++)
        {
            if (j == i)
            {
                ItemOn[j] = true;
            }
            else
            {
                ItemOn[j] = false;
            }
        }
    }
}
