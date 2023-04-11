using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class SaveManager : MonoBehaviour
{
    private string filePath;

    public void Awake()
    {
        filePath = Path.Combine(Application.persistentDataPath, "savedata.json");
    }

    public void OnSave(string playingStageName, string clearStageName,  bool isClear)
    {
        string json = File.ReadAllText(filePath);

        SaveData saveData = JsonUtility.FromJson<SaveData>(json);

        if (clearStageName == "Tutorial")
        {
            clearStageName = "Stage1";
        }

        saveData.stageName = playingStageName;

        if (isClear)
        {
            saveData.clearListNames.Add(clearStageName); 
        }

        json = JsonUtility.ToJson(saveData);
        File.WriteAllText(filePath, json);
    }

    public SaveData LoadSave()
    {
        try
        {
            string json = File.ReadAllText(filePath);
            return JsonUtility.FromJson<SaveData>(json);
        }

        catch (System.Exception)
        {
            File.WriteAllText(filePath, JsonUtility.ToJson(new SaveData ("None", new List<string>())));

            string json = File.ReadAllText(filePath);
            return JsonUtility.FromJson<SaveData>(json);
        }
    }

    public void ClearSave()
    {
        File.WriteAllText(filePath, JsonUtility.ToJson(new SaveData ("None", new List<string>())));
    }
}

[Serializable]
public class SaveData
{
    public string stageName = "None";

    public List<string> clearListNames = new List<string>();

    public SaveData(string stageName, List<string> clearListNames)
    {
        this.stageName = stageName;
        this.clearListNames = clearListNames;
    }
}