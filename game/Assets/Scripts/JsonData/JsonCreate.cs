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
    string InputPath;
    string fullPath;
    float[,] value = new float[5,5];
    float[,] multiple = new float[4, 5];

    [MenuItem("StageMaker/EnemyStatus")]
    static void Open()
    {
        EditorWindow.GetWindow<JsonCreate>("EnemyStatus");
    }

    private void OnGUI()
    {
        minSize = new Vector2(800, 500);
        int instanceID = Selection.activeInstanceID;
        string path = AssetDatabase.GetAssetPath(instanceID);
        fullPath = Path.GetFullPath(path);

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
            EditorGUIUtility.labelWidth = 50;
            EditData();
            
        }

        if (GUILayout.Button("Multiple"))
        {
            IsMultiple = true;
            LoadMultipleData(fullPath);
        }
        if (IsMultiple)
        {
            EditorGUIUtility.labelWidth = 50;
            MultipleData(fullPath);
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
        InputPath = EditorGUILayout.TextField("ƒtƒ@ƒCƒ‹–¼‚Ì“ü—Í", InputPath);
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
        for (int i = 0; i < 5; i++)
        {
            value[i, 0] = state.status[i].hp;
            value[i, 1] = state.status[i].damage;
            value[i, 2] = state.status[i].appear;
            value[i, 3] = state.status[i].score;
            value[i, 4] = state.status[i].speed;
        }
    }

    void LoadMultipleData(string Path)
    {
        LoadData(Path);
        for(int i = 0; i < 4; i++)
        {
            for(int j = 0; j < 5; j++)
            {
                multiple[i, j] = value[i + 1, j] / value[0, j];
            }
        }
    }

    void MultipleData(string Path)
    {
        for (int i = 0; i < 4; i++)
        {
            EditorGUILayout.BeginHorizontal();
            {
                multiple[i, 0] = EditorGUILayout.FloatField("HP", multiple[i, 0]);
                multiple[i, 1] = EditorGUILayout.FloatField("Damage", multiple[i, 1]);
                multiple[i, 2] = EditorGUILayout.FloatField("Appear", multiple[i, 2]);
                multiple[i, 3] = EditorGUILayout.FloatField("Score", multiple[i, 3]);
                multiple[i, 4] = EditorGUILayout.FloatField("Speed", multiple[i, 4]);
            }
            EditorGUILayout.EndHorizontal();
        }

    }
    void EditData()
    {
        for(int i = 0; i < 5; i++)
        {
            EditorGUILayout.BeginHorizontal();
            {
                value[i,0] = EditorGUILayout.FloatField("HP", value[i,0]);
                value[i,1] = EditorGUILayout.FloatField("Damage", value[i, 1]);
                value[i,2] = EditorGUILayout.FloatField("Appear", value[i, 2]);
                value[i, 3] = EditorGUILayout.FloatField("Score", value[i, 3]);
                value[i,4] = EditorGUILayout.FloatField("Speed", value[i, 4]);
            }
            EditorGUILayout.EndHorizontal();
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
public class Status
{
    public string dateAndTime;
    public SaveProperty[] status = new SaveProperty[5];
}


