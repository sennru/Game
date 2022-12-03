using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class UnityExtension2 : EditorWindow
{
    [MenuItem("Window/EditorEx")]
    static void Open()
    {
        EditorWindow.GetWindow<UnityExtension2>("EditorEx");
    }

    Vector3 ScaleSize = Vector3.zero;
    int Second;
    float MoveScale;
    Animation anim;

    private void OnGUI()
    {
        EditorGUILayout.LabelField("キューブの大きさ");
        ScaleSize = EditorGUILayout.Vector3Field("大きさ", ScaleSize);
        EditorGUILayout.LabelField("スライドの往復速度(往復s)");
        Second = EditorGUILayout.IntField("往復までの時間", Second);
        EditorGUILayout.LabelField("スライドの大きさ");
        MoveScale = EditorGUILayout.FloatField("大きさ", MoveScale);

        if (GUILayout.Button("CloneStart"))
        {
            GameObject CloneItem = GameObject.Find("SlideBlock");
            var clone = GameObject.Instantiate(CloneItem);
            clone.transform.localScale = ScaleSize;
            
        }
    }
}
