using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Jsononly : MonoBehaviour
{
    private void Start()
    {
        Status state = loadPlayerData(Application.dataPath + "/save2.json");
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
}
