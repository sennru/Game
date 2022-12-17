using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using CaluculateExtention;

public class StageMaker : EditorWindow
{
    float Unit;
    float[] UnitControll = {0.01f, 0.05f, 0.1f, 0.2f, 0.5f, 1f, 2f, 3f, 4f, 5f, 10f, 25f, 50f, 100f};
    int floatIndex = 5;
    int PatternCount;
    bool SpecifyDatamArea = false;
    bool[] MirrorAxis = new bool[3];
    bool DistancePattern = true;
    bool RotatePattern;
    bool FixedDirection;
    string PackageName;
    string OriginalName;
    GameObject MirrorPlate;
    GameObject PackageObject;
    GameObject[] PrehubObject;
    Vector3 DatamVector = Vector3.zero;
    Vector3 PatternVector;
    Vector3 PatternRotateVector = Vector3.zero;

    [MenuItem("StageMaker/Create")]
    static void Open()
    {
        EditorWindow.GetWindow<StageMaker>("StageMaker");
        
    }

    void OnGUI()
    {
        minSize = new Vector2(500, 600);
        EditorGUILayout.LabelField("座標移動");
        ControllPosition();
        Unit = EditorGUILayout.FloatField("大きさ", UnitControll[floatIndex]);
        ControllUnit();
        EditorGUILayout.LabelField("\n");
        SpecifyDatamArea = EditorGUILayout.Toggle("指定した面でミラー", SpecifyDatamArea);
        Mirror();
        EditorGUILayout.LabelField("\n");
        EditorGUILayout.LabelField("パターン化");
        Pattern();
        EditorGUILayout.LabelField("\n");
        EditorGUILayout.LabelField("複数のオブジェクトに名前をつける");
        Named();
        EditorGUILayout.LabelField("\n");
        EditorGUILayout.LabelField("入れ替え (個数を1:1に対応してください)");
        ReplaceObject();
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
            GameObject MirrorObject = new GameObject();
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
                GameObject.Instantiate(n, pos, Quaternion.identity).transform.parent = MirrorObject.transform;
            }
            MirrorObject.name = "MirrorCopies";
        }
    }

    void Pattern()
    {
        GameObject CopyObject;
        PatternCount = EditorGUILayout.IntField("生成するパターン数", PatternCount);
        EditorGUILayout.BeginHorizontal();
        {
            DistancePattern = EditorGUILayout.Toggle("距離でパターンコピー", DistancePattern);
            RotatePattern = EditorGUILayout.Toggle("回転でコピー", RotatePattern);
        }
        EditorGUILayout.EndHorizontal();

        if(DistancePattern == true)
        {
            PatternVector = EditorGUILayout.Vector3Field("パターンの方向", PatternVector);
        }
        else if(RotatePattern == true)
        {
            FixedDirection = EditorGUILayout.Toggle("向きを固定", FixedDirection);
            PatternRotateVector = EditorGUILayout.Vector3Field("パターンの回転方向", PatternRotateVector);
        }

        PackageName = EditorGUILayout.TextField("まとめる時の名前", PackageName);

        if (GUILayout.Button("PatternCopy") && PatternCount > 0 && PackageName != null && DistancePattern ^ RotatePattern)
        {
            int Index = 0;
            PackageObject = new GameObject(PackageName);
            foreach (var n in Selection.gameObjects)
            {
                Vector3 pos = n.transform.position;
                for (int i = 1; i <= PatternCount; i++)
                {
                    Index++;
                    CopyObject = GameObject.Instantiate(n, pos, Quaternion.identity);
                    if (DistancePattern == true)
                    {
                        CopyObject.transform.position += PatternVector * i;
                    }
                    else if(RotatePattern == true)
                    {
                        CopyObject.transform.RotateAround(Vector3.zero, Vector3.right, PatternRotateVector.x * i);
                        CopyObject.transform.RotateAround(Vector3.zero, Vector3.up, PatternRotateVector.y * i);
                        CopyObject.transform.RotateAround(Vector3.zero, Vector3.forward, PatternRotateVector.z * i);
                        if(FixedDirection == true)
                        {
                            CopyObject.transform.rotation = Quaternion.identity;
                        }
                    }
                    CopyObject.name += "_" + Index.ToString();
                    CopyObject.transform.parent = PackageObject.transform;
                }
            }
        }
        else if(PackageName == null)
        {
            Debug.Log("名前を入力してください");
        }
        else if(DistancePattern ^ RotatePattern == false)
        {
            Debug.Log("どちらかのパターンを選択してください");
        }
    }

    void Named()
    {
        OriginalName = EditorGUILayout.TextField("元の名前", OriginalName);
        if (GUILayout.Button("Add Index"))
        {
            int index = 0;
            foreach (var n in Selection.gameObjects)
            {
                index++;
                n.name = OriginalName + "_" + index;
            }
        }
    }

    void ReplaceObject()
    {
        if (Selection.gameObjects != null && GUILayout.Button("PrehubSelect"))
        {
            PrehubObject = Selection.gameObjects;
            EditorGUILayout.LabelField("入れ替え先を選んでください");
        }

        if (PrehubObject != null && Selection.gameObjects != null && GUILayout.Button("ReplaceStart"))
        {
            int index = 0;
            Vector3 PrehubPos;
            GameObject[] ReplacedObjects = Selection.gameObjects;
            GameObject ReplacedEmptyObject = new GameObject();
            ReplacedEmptyObject.name = "ReplacedObjects";
            for (int i = 0; i < ReplacedObjects.Length; i++)
            {
                PrehubPos = PrehubObject[index].transform.position;
                PrehubObject[index].transform.position = ReplacedObjects[index].transform.position;
                ReplacedObjects[index].transform.position = PrehubPos;
                PrehubObject[index].transform.parent = ReplacedObjects[index].transform.parent;
                ReplacedObjects[index].transform.parent = ReplacedEmptyObject.transform;

                index++;
            }
        }
    }
}
