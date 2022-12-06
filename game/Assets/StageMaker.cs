using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class StageMaker : EditorWindow
{
    float Unit;
    float[] UnitControll = {0.01f, 0.05f, 0.1f, 0.2f, 0.5f, 1f, 2f, 3f, 4f, 5f, 10f, 25f, 50f, 100f};
    int floatIndex = 5;
    bool SpecifyDatamArea = false;
    bool[] MirrorAxis = new bool[3];
    GameObject MirrorPlate;
    Vector3 DatamVector = Vector3.zero;

    [MenuItem("Edit/StageMaker")]
    static void Open()
    {
        EditorWindow.GetWindow<StageMaker>("StageMaker");
    }

    void OnGUI()
    {
        EditorGUILayout.LabelField("座標移動");
        ControllPosition();
        Unit = EditorGUILayout.FloatField("大きさ", UnitControll[floatIndex]);
        ControllUnit();
        SpecifyDatamArea = EditorGUILayout.Toggle("指定した面でミラー", SpecifyDatamArea);
        Mirror();
    }

    void ControllPosition()
    {
        EditorGUILayout.BeginHorizontal();
        {
            if (GUILayout.Button("X▲", EditorStyles.miniButtonLeft))
            {
                MovePosition("X▲");
            }
            if (GUILayout.Button("Y▲", EditorStyles.miniButtonMid))
            {
                MovePosition("Y▲");
            }
            if (GUILayout.Button("Z▲", EditorStyles.miniButtonRight))
            {
                MovePosition("Z▲");
            }
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        {
            if (GUILayout.Button("X▼", EditorStyles.miniButtonLeft))
            {
                MovePosition("X▼");
            }
            if (GUILayout.Button("Y▼", EditorStyles.miniButtonMid))
            {
                MovePosition("Y▼");
            }
            if (GUILayout.Button("Z▼", EditorStyles.miniButtonRight))
            {
                MovePosition("Z▼");
            }
        }
        EditorGUILayout.EndHorizontal();
    }

    void MovePosition(string Move)
    {
        if (Selection.gameObjects != null && 0 < Selection.gameObjects.Length)
        {
            var list = new List<Object>();
            foreach (var n in Selection.gameObjects)
            {
                Vector3 pos = n.transform.localPosition;
                switch (Move)
                {
                    case "X▲":
                        pos.x += Unit;
                        n.transform.localPosition = pos;
                        break;
                    case "Y▲":
                        pos.y += Unit;
                        n.transform.localPosition = pos;
                        break;
                    case "Z▲":
                        pos.z += Unit;
                        n.transform.localPosition = pos;
                        break;
                    case "X▼":
                        pos.x -= Unit;
                        n.transform.localPosition = pos;
                        break;
                    case "Y▼":
                        pos.y -= Unit;
                        n.transform.localPosition = pos;
                        break;
                    case "Z▼":
                        pos.z -= Unit;
                        n.transform.localPosition = pos;
                        break;
                }
            }
        }
    }

    void ControllUnit()
    {
        EditorGUILayout.BeginHorizontal();
        {
            if (GUILayout.Button("←", EditorStyles.miniButtonLeft) && floatIndex > 0)
            {
                floatIndex -= 1;
                Unit = UnitControll[floatIndex];
            }
            if (GUILayout.Button("→", EditorStyles.miniButtonRight) && floatIndex < 13)
            {
                floatIndex += 1;
                Unit = UnitControll[floatIndex];
            }
        }
        EditorGUILayout.EndHorizontal();
    }

    void Mirror()
    {
        MirrorAxis[0] = EditorGUILayout.Toggle("X平面対称", MirrorAxis[0]);
        MirrorAxis[1] = EditorGUILayout.Toggle("Y平面対称", MirrorAxis[1]);
        MirrorAxis[2] = EditorGUILayout.Toggle("Z平面対称", MirrorAxis[2]);
        if (SpecifyDatamArea == true)
        {
            if(MirrorPlate == null)
            {
                MirrorPlate = GameObject.CreatePrimitive(PrimitiveType.Cube);
            }
            //MirrorPlate.GetComponent<Renderer>().material.color = new Color32(255, 255, 255, 50);

            Vector3 scale = MirrorPlate.transform.localScale;
            if(MirrorAxis[0] == true)
            {
                scale.y = 0.5f;
                MirrorPlate.transform.localScale = scale;
            }
            else
            {
                scale.y = 100f;
                MirrorPlate.transform.localScale = scale;
            }
            if (MirrorAxis[1] == true)
            {
                scale.z = 0.5f;
                MirrorPlate.transform.localScale = scale;
            }
            else
            {
                scale.z = 100f;
                MirrorPlate.transform.localScale = scale;
            }
            if (MirrorAxis[2] == true)
            {
                scale.x = 0.5f;
                MirrorPlate.transform.localScale = scale;
            }
            else
            {
                scale.x = 100f;
                MirrorPlate.transform.localScale = scale;
            }
            DatamVector = EditorGUILayout.Vector3Field("ミラー面の座標", DatamVector);
            MirrorPlate.transform.position = DatamVector;
        }
        else if(SpecifyDatamArea == false && MirrorPlate != null)
        {
            DestroyImmediate(MirrorPlate);
        }

        if (GUILayout.Button("CreateMirror"))
        {
            foreach (var n in Selection.gameObjects)
            {
                Vector3 pos = n.transform.position;
                if (SpecifyDatamArea == true)
                {
                    if (MirrorAxis[0] == true)
                    {
                        pos.y -= (n.transform.position.y - MirrorPlate.transform.position.y) * 2;
                    }
                    if (MirrorAxis[1] == true)
                    {
                        pos.z -= (n.transform.position.z - MirrorPlate.transform.position.z) * 2;
                    }
                    if (MirrorAxis[2] == true)
                    {
                        pos.x -= (n.transform.position.x - MirrorPlate.transform.position.x) * 2;
                    }
                }
                else
                {
                    if (MirrorAxis[0] == true)
                    {
                        pos.y *= -1;
                    }
                    if (MirrorAxis[1] == true)
                    {
                        pos.z *= -1;
                    }
                    if (MirrorAxis[2] == true)
                    {
                        pos.x *= -1;
                    }
                }
                Instantiate(n, pos, Quaternion.identity);
            }
        }
    }
}
