using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using CaluculateExtention;

public class Measure : EditorWindow
{
    [MenuItem("StageMaker/Measure")]
    static void Open()
    {
        EditorWindow.GetWindow<Measure>("Measure");
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("2“_ŠÔ‚Ì‹——£‚ð‘ª’è");
        MeasureDistance();
    }

    void MeasureDistance()
    {
        var dis = PositionCalculate.Distance(Selection.gameObjects[0].transform.position, Selection.gameObjects[1].transform.position);
        if (GUILayout.Button("MeasureDistance") && Selection.gameObjects.Length == 2)
        {
            Debug.Log(dis.ToString());
        }
    }
}
