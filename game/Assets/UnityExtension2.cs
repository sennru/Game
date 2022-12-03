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
        EditorGUILayout.LabelField("�L���[�u�̑傫��");
        ScaleSize = EditorGUILayout.Vector3Field("�傫��", ScaleSize);
        EditorGUILayout.LabelField("�X���C�h�̉������x(����s)");
        Second = EditorGUILayout.IntField("�����܂ł̎���", Second);
        EditorGUILayout.LabelField("�X���C�h�̑傫��");
        MoveScale = EditorGUILayout.FloatField("�傫��", MoveScale);

        if (GUILayout.Button("CloneStart"))
        {
            GameObject CloneItem = GameObject.Find("SlideBlock");
            var clone = GameObject.Instantiate(CloneItem);
            clone.transform.localScale = ScaleSize;
            
        }
    }
}
