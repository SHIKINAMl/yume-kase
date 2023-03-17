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

        saveData.stageName = playingStageName;


        saveData.clearList[saveData.GetIndexFromName(clearStageName)].isClear = isClear;

        json = JsonUtility.ToJson(saveData);
        File.WriteAllText(filePath, json);
    }
}

[Serializable]
public class SaveData
{
    public string stageName;

    [SerializeField]
    public List<ClearData> clearList;

    public SaveData(string stageName, List<ClearData> clearList)
    {
        this.stageName = stageName;
        this.clearList = clearList;
    }

    public int GetIndexFromName(string stageName)
    {
        for (int i = 0; i < clearList.Count; i++)
        {
            if (clearList[i].stageName == stageName)
            {
                return i;
            }
        }

        return -1;
    }
}

[Serializable]
public class ClearData
{
    public string stageName;
    public bool isClear;

    public ClearData(string stageName, bool isClear)
    {
        this.stageName = stageName;
        this.isClear = isClear;
    }
}
