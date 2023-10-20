using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class SaveManager : MonoBehaviour
{
    private string filePath => Path.Combine(Application.persistentDataPath, "savedata.json");

    public void Awake()
    {
        //filePath = Path.Combine(Application.persistentDataPath, "savedata.json");
    }

    public void OnSave(string playingStageName, string clearStageName,  bool isClear)
    {
        string json = File.ReadAllText(filePath);

        SaveData saveData = JsonUtility.FromJson<SaveData>(json);

        /*
        if (clearStageName == "Tutorial")
        {
            clearStageName = "Stage1";
        }
        */

        saveData.stageName = playingStageName;

        if (isClear)
        {
            if (saveData.clearListNames.IndexOf(clearStageName) == -1)
            {
                saveData.clearListNames.Add(clearStageName);  
            }
        }

        json = JsonUtility.ToJson(saveData);
        File.WriteAllText(filePath, json);
    }

    public void OnSaveSetting(List<float> newSettingData)
    {
        string json = File.ReadAllText(filePath);

        SaveData saveData = JsonUtility.FromJson<SaveData>(json);

        saveData.settingData = newSettingData;

        json = JsonUtility.ToJson(saveData);
        File.WriteAllText(filePath, json);
    }

    public SaveData LoadSave()
    {
        try
        {
            string json = File.ReadAllText(filePath);

            SaveData saveData = JsonUtility.FromJson<SaveData>(json);
            
            if (saveData.stageName == null || saveData.stageName == "None")
            {
                saveData.stageName = "None";
            }

            if (saveData.settingData.Count != 3)
            {
                saveData.settingData = new List<float>() {1, 1, 1};
                json = JsonUtility.ToJson(saveData);
                File.WriteAllText(filePath, json);
            }

            return JsonUtility.FromJson<SaveData>(json);
        }

        catch (System.Exception)
        {
            File.WriteAllText(filePath, JsonUtility.ToJson(new SaveData ("None", new List<string>(), new List<float>() {1, 1, 1})));

            string json = File.ReadAllText(filePath);
            return JsonUtility.FromJson<SaveData>(json);
        }
    }

    public void ClearSave()
    {
        string json = File.ReadAllText(filePath);

        SaveData saveData = JsonUtility.FromJson<SaveData>(json);


        saveData.stageName = "None";

        saveData.clearListNames = new List<string>();

        json = JsonUtility.ToJson(saveData);
        File.WriteAllText(filePath, json);
    }
}

[Serializable]
public class SaveData
{
    public string stageName = "None";

    public List<string> clearListNames = new List<string>();

    public List<float> settingData = new List<float>() {1, 1, 1};

    public SaveData(string stageName, List<string> clearListNames, List<float> settingData)
    {
        this.stageName = stageName;
        this.clearListNames = clearListNames;
        this.settingData = settingData;
    }
}