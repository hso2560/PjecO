using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using System;

public class JsonSaveMng : MonoBehaviour
{
    readonly string fileName = "SaveFile.txt";
    string filePath;
    string savedJson;
    PlayerInfo info;

    private void Awake()
    {
        info = new PlayerInfo();
        filePath = string.Concat(Application.persistentDataPath, "/", fileName);
        //Load();
    }

    public void Save()
    {
        savedJson = JsonUtility.ToJson(info);
        byte[] bytes = Encoding.UTF8.GetBytes(savedJson);
        string code = Convert.ToBase64String(bytes);
        File.WriteAllText(filePath, code);
    }
    public void Load()
    {
        if(File.Exists(filePath))
        {
            string code=File.ReadAllText(filePath);
            byte[] bytes = Convert.FromBase64String(code);
            savedJson = Encoding.UTF8.GetString(bytes);
            info = JsonUtility.FromJson<PlayerInfo>(savedJson);
        }
    }


    /*private void OnApplicationQuit()
    {
        Save();
    }*/
}
