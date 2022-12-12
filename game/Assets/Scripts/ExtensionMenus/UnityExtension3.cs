using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class UnityExtension3 : EditorWindow
{
    [MenuItem("StageMaker/MakeBox")]
    static void Open()
    {
        EditorWindow.GetWindow<UnityExtension3>("MakeBox");
    }

    Vector4 BoxSize = Vector4.zero;
    // Update is called once per frame
    void OnGUI()
    {
        EditorGUILayout.LabelField("ÉLÉÖÅ[ÉuÇÃëÂÇ´Ç≥");
        BoxSize = EditorGUILayout.Vector4Field("ëÂÇ´Ç≥", BoxSize);
        float x = BoxSize.x;
        float y = BoxSize.y;
        float z = BoxSize.z;
        float t = BoxSize.w;

        bool Limit = false;
        Vector3[] LimitScale = new Vector3[5];
        LimitScale[0] = new Vector3(t, y, z - 2f * t);
        LimitScale[1] = new Vector3(x, y, t);
        LimitScale[2] = new Vector3(t, y, z - 2f * t);
        LimitScale[3] = new Vector3(x, y, t);
        LimitScale[4] = new Vector3(x - 2f * t, t, z - 2f * t);

        for(int i = 0; i < LimitScale.Length; i++)
        {
            if (LimitScale[i].x > 0 && LimitScale[i].y > 0 && LimitScale[i].z > 0)
            {
                Limit = true;
            }
            else
            {
                Limit = false;
                break;
            }
        }

        if (GUILayout.Button("BoxCreate") && Limit == true)
        {
            
            Vector3[] CubePosition = new Vector3[5];
            GameObject CloneItem = GameObject.CreatePrimitive(PrimitiveType.Cube);
            GameObject[] CloneCube = new GameObject[5];
            for(int i = 0; i < CloneCube.Length; i++)
            {
                CloneCube[i] = GameObject.Instantiate(CloneItem);
            }
            CloneCube[0].transform.localPosition = new Vector3((x - t) / 2f, y / 2f, 0f);
            CloneCube[1].transform.localPosition = new Vector3(0f, y / 2f, (z - t) / 2f);
            CloneCube[2].transform.localPosition = new Vector3(-(x - t) / 2f, y / 2f, 0f);
            CloneCube[3].transform.localPosition = new Vector3(0f, y / 2f, -(z - t) / 2f);
            CloneCube[4].transform.localPosition = new Vector3(0f, y - (t / 2f), 0f);

            for (int i = 0; i < CloneCube.Length; i++)
            {
                CloneCube[i].transform.localScale = LimitScale[i];
            }
        }
    }
}
