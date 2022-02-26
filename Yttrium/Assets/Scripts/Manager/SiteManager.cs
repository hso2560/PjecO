using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using System.IO;

public class SiteManager : MonoSingleton<SiteManager>
{
    private string savedJson, filePath;
    private readonly string saveFileName_1 = "SaveFile1";

    [SerializeField] private SaveData saveData;
    public SaveData savedData { get { return saveData; } }

    private void Awake()
    {
        filePath = saveFileName_1.PersistentDataPath();
        saveData = new SaveData();
        Load();
    }

    private void SaveData()
    {
        saveData.Save();
    }

    private void Save()
    {
        SaveData();

        savedJson = JsonUtility.ToJson(saveData);
        byte[] bytes = Encoding.UTF8.GetBytes(savedJson);
        string code = Convert.ToBase64String(bytes);
        File.WriteAllText(filePath, code);
    }

    private void Load()
    {
        if (File.Exists(filePath))
        {
            string code = File.ReadAllText(filePath);
            byte[] bytes = Convert.FromBase64String(code);
            savedJson = Encoding.UTF8.GetString(bytes);
            saveData = JsonUtility.FromJson<SaveData>(savedJson);
        }

        saveData.Load();
        SetData();
    }

    private void SetData()
    {

    }


    private void OnApplicationQuit()
    {
        
    }
    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {

        }
    }
    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {

        }
    }
}
