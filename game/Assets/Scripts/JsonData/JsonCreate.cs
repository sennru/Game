using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;

public class JsonCreate : EditorWindow
{
    bool Loading = false;
    bool newData = false;
    bool IsMultiple = false;
    bool[] paramDisplay = new bool[5];
    string InputPath;
    string fullPath;
    string[] SDname = new string[4];
    string[] GDname = new string[3];
    string[] SCname = new string[3];
    string[] GCname = new string[3];
    float[,] value = new float[5,5];
    float[,] multiple = new float[4, 5];
    public float[] swordDamage = new float[4];
    public float[] gunDamage = new float[3];
    public int[] swordCost = new int[3];
    public int[] gunCost = new int[3];

    [MenuItem("StageMaker/EnemyStatus/EditFile")]
    static void Open()
    {
        EditorWindow.GetWindow<JsonCreate>("EnemyStatus");
    }

    private void OnGUI()
    {
        minSize = new Vector2(800, 700);
        int instanceID = Selection.activeInstanceID;
        string path = AssetDatabase.GetAssetPath(instanceID);
        fullPath = Path.GetFullPath(path);
        NameStrings();

        if (GUILayout.Button("NewData"))
        {
            newData = true;
        }
        if (newData)
        {
            NewData();
        }

        if (GUILayout.Button("LoadData"))
        {
            LoadData(fullPath);
            Loading = true;
        }
        if (Loading)
        {
            EditorGUIUtility.labelWidth = 100;
            EditorGUIUtility.fieldWidth = 50;
            EditData();
        }

        if (GUILayout.Button("SaveData"))
        {
            if (IsMultiple == false)
            {
                SaveData(fullPath);
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        value[i + 1, j] = value[0, j] * multiple[i, j];
                    }
                }
                SaveData(fullPath);
            }
        }

        if (GUILayout.Button("CopyJson"))
        {
            CreateCopy();
        }

        ComperisonParam();
    }

    public void savePlayerData(Status state, string Path)
    {
        StreamWriter writer;
        DateTime TodayNow;
        TodayNow = DateTime.Now;
        var Today = TodayNow.Year.ToString() + "/" + TodayNow.Month.ToString() + "/" + TodayNow.Day.ToString() + "/" + DateTime.Now.ToLongTimeString();
        state.dateAndTime = Today;
        
        string jsonstr = JsonUtility.ToJson(state, true);
        writer = new StreamWriter(Path, false);
        writer.Write(jsonstr);
        writer.Flush();
        writer.Close();
    }

    public Status loadPlayerData(string Path)
    {
        string datastr;
        StreamReader reader;
        reader = new StreamReader(Path);
        datastr = reader.ReadToEnd();
        reader.Close();
        return JsonUtility.FromJson<Status>(datastr);
    }

    void NewData()
    {
        InputPath = EditorGUILayout.TextField("ファイル名の入力", InputPath);
        if (GUILayout.Button("JsonCreate"))
        {
            Status state = new Status();
            savePlayerData(state, Application.dataPath + "/" + InputPath + ".json");
            this.Close();
        }
    }

    void LoadData(string Path)
    {
        Status state = loadPlayerData(Path);
        for (int i = 0; i < state.status.Length; i++)
        {
            value[i, 0] = state.status[i].hp;
            value[i, 1] = state.status[i].damage;
            value[i, 2] = state.status[i].appear;
            value[i, 3] = state.status[i].score;
            value[i, 4] = state.status[i].speed;
        }
        swordDamage = state.property.swordDamage;
        gunDamage = state.property.gunDamage;
        swordCost = state.property.swordCost;
        gunCost = state.property.gunCost;
    }

    void EditData()
    {
        paramDisplay[0] = EditorGUILayout.Foldout(paramDisplay[0], "EnemyParamator");
        if (paramDisplay[0])
        {
            for (int i = 0; i < 5; i++)
            {
                EditorGUILayout.BeginHorizontal();
                {
                    value[i, 0] = EditorGUILayout.FloatField("HP", value[i, 0]);
                    value[i, 1] = EditorGUILayout.FloatField("Damage", value[i, 1]);
                    value[i, 2] = EditorGUILayout.FloatField("Appear", value[i, 2]);
                    value[i, 3] = EditorGUILayout.FloatField("Score", value[i, 3]);
                    value[i, 4] = EditorGUILayout.FloatField("Speed", value[i, 4]);
                }
                EditorGUILayout.EndHorizontal();
            }
        }
        paramDisplay[1] = EditorGUILayout.Foldout(paramDisplay[1], "SwordDamage");
        if (paramDisplay[1])
        {
            for (int i = 0; i < swordDamage.Length; i++)
            {
                swordDamage[i] = EditorGUILayout.FloatField(SDname[i], swordDamage[i]);
            }
        }
        paramDisplay[2] = EditorGUILayout.Foldout(paramDisplay[2], "GunDamage");
        if (paramDisplay[2])
        {
            for (int i = 0; i < gunDamage.Length; i++)
            {
                gunDamage[i] = EditorGUILayout.FloatField(GDname[i], gunDamage[i]);
            }
        }
        paramDisplay[3] = EditorGUILayout.Foldout(paramDisplay[3], "SwordCost");
        if (paramDisplay[3])
        {
            for (int i = 0; i < swordCost.Length; i++)
            {
                swordCost[i] = EditorGUILayout.IntField(SCname[i], swordCost[i]);
            }
        }
        paramDisplay[4] = EditorGUILayout.Foldout(paramDisplay[4], "GunCost");
        if (paramDisplay[4])
        {
            for (int i = 0; i < gunCost.Length; i++)
            {
                gunCost[i] = EditorGUILayout.IntField(GCname[i], gunCost[i]);
            }
        }
    }
    void SaveData(string Path)
    {
        Status state = loadPlayerData(Path);
        for(int i = 0; i < 5; i++)
        {
            for(int j = 0; j < 5; j++)
            {
                value[i, j] *= 100;
                value[i, j] = Mathf.Floor(value[i, j]);
                value[i, j] /= 100;
                
            }
            state.status[i].hp = float.Parse(value[i, 0].ToString("f2"));
            state.status[i].damage = float.Parse(value[i, 1].ToString("f2"));
            state.status[i].appear = float.Parse(value[i, 2].ToString("f2"));
            state.status[i].score = (int)value[i, 3];
            state.status[i].speed = float.Parse(value[i, 4].ToString("f2"));
        }
        state.property.swordDamage = swordDamage;
        state.property.gunDamage = gunDamage;
        state.property.swordCost = swordCost;
        state.property.gunCost = gunCost;
        savePlayerData(state, Path);
    }
    static void CreateCopy()
    {
        foreach (var obj in Selection.objects)
        {
            var path = AssetDatabase.GetAssetPath(obj);
            if (path == string.Empty)
            {
                var gameObject = obj as GameObject;
                var copy = GameObject.Instantiate(gameObject, gameObject.transform.parent);
                copy.name = obj.name;
                copy.transform.SetSiblingIndex(gameObject.transform.GetSiblingIndex());
                Undo.RegisterCreatedObjectUndo(copy, "deplicate");
            }
            else
            {
                var newPath = AssetDatabase.GenerateUniqueAssetPath(path);
                AssetDatabase.CopyAsset(path, newPath);
            }
        }
    }

    void NameStrings()
    {
        for(int i = 0; i < swordDamage.Length; i++)
        {
            SDname[i] = "SwordDamage" + i.ToString();
        }
        for (int i = 0; i < 3; i++)
        {
            GDname[i] = "GunDamage" + i.ToString();
            SCname[i] = "SwordCost" + i.ToString();
            GCname[i] = "GunCost" + i.ToString();
        }
    }
    private static void ComperisonParam()
    {
        if (GUILayout.Button("ShowComperison"))
        {
            var window = EditorWindow.GetWindow<Comparison>("Comparison");
            window.ShowAuxWindow();
        }
    }
}

[System.Serializable]
public class SaveProperty
{
    public float hp;
    public float damage;
    public float appear;
    public int score;
    public float speed;
}

[System.Serializable]
public class DamageAndCost
{
    public float[] swordDamage = new float[4];
    public float[] gunDamage = new float[3];
    public int[] swordCost = new int[3];
    public int[] gunCost = new int[3];

}

public class Comparison : EditorWindow
{
    string fullPath;
    bool[] Calculate = new bool[2];
    bool[] DamageCauculate = new bool[7];
    bool[] CostCauculate = new bool[6];
    string[] SDname = new string[4];
    string[] GDname = new string[3];
    string[] SCname = new string[3];
    string[] GCname = new string[3];
    float[,] value = new float[5, 5];
    float[,] multiple = new float[4, 5];
    public float[] swordDamage = new float[4];
    public float[] gunDamage = new float[3];
    public int[] swordCost = new int[3];
    public int[] gunCost = new int[3];
    Vector2  _scrollPosition = Vector2.zero;

    private void OnGUI()
    {
        _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);
        minSize = new Vector2(800, 700);
        int instanceID = Selection.activeInstanceID;
        string path = AssetDatabase.GetAssetPath(instanceID);
        fullPath = Path.GetFullPath(path);
        NameStrings();
        LoadData(fullPath);
        DamageCal();
        EditorGUILayout.EndScrollView();
    }

    void LoadData(string Path)
    {
        Status state = loadPlayerData(Path);
        for (int i = 0; i < state.status.Length; i++)
        {
            value[i, 0] = state.status[i].hp;
            value[i, 1] = state.status[i].damage;
            value[i, 2] = state.status[i].appear;
            value[i, 3] = state.status[i].score;
            value[i, 4] = state.status[i].speed;
        }
        swordDamage = state.property.swordDamage;
        gunDamage = state.property.gunDamage;
        swordCost = state.property.swordCost;
        gunCost = state.property.gunCost;
    }

    public Status loadPlayerData(string Path)
    {
        string datastr;
        StreamReader reader;
        reader = new StreamReader(Path);
        datastr = reader.ReadToEnd();
        reader.Close();
        return JsonUtility.FromJson<Status>(datastr);
    }
    void DamageCal()
    {
        Calculate[0] = EditorGUILayout.Foldout(Calculate[0], "Damage");
        if (Calculate[0])
        {
            EditorGUI.indentLevel = 1;
            for (int i = 0; i < 4; i++)
            {
                DamageCauculate[i] = EditorGUILayout.Foldout(DamageCauculate[i], SDname[i]);
                if (DamageCauculate[i])
                {
                    EditorGUI.indentLevel = 2;
                    for (int j = 0; j < 5; j++)
                    {

                        if(i < 2)
                        {
                            EditorGUILayout.LabelField("Enemy" + j + "は" + (value[j, 0] / swordDamage[i]).ToString("F2") + "回で倒せます");
                        }
                        else
                        {
                            EditorGUILayout.LabelField("Enemy" + j + "は" + (value[j, 0] / (swordDamage[i] * 10)).ToString("F2") + "回で倒せます");
                        }
                    }
                    EditorGUI.indentLevel = 1;
                }
            }
            for(int i = 0; i < 3; i++)
            {
                DamageCauculate[i + 4] = EditorGUILayout.Foldout(DamageCauculate[i + 4], GDname[i]);
                if (DamageCauculate[i + 4])
                {
                    EditorGUI.indentLevel = 2;
                    for (int j = 0; j < 5; j++)
                    {
                        EditorGUILayout.LabelField("Enemy" + j + "は" + (value[j, 0] / gunDamage[i]).ToString("F2") + "発で倒せます");
                    }
                    EditorGUI.indentLevel = 1;
                }

            }
        }
        EditorGUI.indentLevel = 0;
        Calculate[1] = EditorGUILayout.Foldout(Calculate[1], "Cost");
        if (Calculate[1])
        {
            EditorGUI.indentLevel = 1;
            for (int i = 0; i < 3; i++)
            {
                CostCauculate[i] = EditorGUILayout.Foldout(CostCauculate[i], SCname[i]);
                if (CostCauculate[i])
                {
                    EditorGUI.indentLevel = 2;
                    for (int j = 0; j < 5; j++)
                    {
                        EditorGUILayout.LabelField("この武器を使うにはEnemy" + j + "が" + (value[j, 3] / swordDamage[i]).ToString("F2") + "体必要です");
                    }
                    EditorGUI.indentLevel = 1;
                }
                CostCauculate[i + 3] = EditorGUILayout.Foldout(CostCauculate[i + 3], GCname[i]);
                if (CostCauculate[i + 3])
                {
                    EditorGUI.indentLevel = 2;
                    for (int j = 0; j < 5; j++)
                    {
                        EditorGUILayout.LabelField("この武器を使うにはEnemy" + j + "が" + (value[j, 3] / gunDamage[i]).ToString("F2") + "体必要です");
                    }
                    EditorGUI.indentLevel = 1;
                }
            }
        }
    }

    void NameStrings()
    {
        for (int i = 0; i < swordDamage.Length; i++)
        {
            SDname[i] = "SwordDamage" + i.ToString();
        }
        for (int i = 0; i < 3; i++)
        {
            GDname[i] = "GunDamage" + i.ToString();
            SCname[i] = "SwordCost" + i.ToString();
            GCname[i] = "GunCost" + i.ToString();
        }
    }
}
