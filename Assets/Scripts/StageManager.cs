using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class StageManager : MonoBehaviour
{
    [SerializeField]
    public int numberOfRoom = 1;
    public int[] sideList = new int[1];

    public int numberOfClearFlag;

    [SerializeField]
    public FlagData[] clearFlagList = new FlagData[1];

    public int numberOfEventFlag;

    [SerializeField]
    public FlagData[] eventFlagList = new FlagData[1];

    public int numberOfBGM;

    [SerializeField]
    public BGMData[] BGMList = new BGMData[1];

    public int numberOfSoundEffect;

    [SerializeField]
    public SoundEffectData[] soundEffectList = new SoundEffectData[1];

    [SerializeField]
    public List<ItemData> itemList = new List<ItemData>();

    public int inventorySize = 6;

    public void Update()
    {
        foreach (var flag in clearFlagList)
        {
            if (flag.flag)
            {
                //SceneManager.LoadScene(flag.GetName());
                Debug.Log(flag.flagName);
            }
        }
    }

    string[] eventFlags = new string[0];
    string[] clearFlags = new string[0];

    public void SetFlagByName(FlagData[] flaglist, string flagName, bool boolean)
    {
        var flags = new List<string>();

        if (flaglist == this.eventFlagList)
        {
            flags = new List<string>(this.eventFlags);
        }
        if (flaglist == this.clearFlagList)
        {
            flags = new List<string>(this.clearFlags);
        }

        if (boolean && !flags.Contains(flagName))
        {
            flags.Add(flagName);
        }
        else if (!boolean && flags.Contains(flagName))
        {
            flags.Remove(flagName);
        }

        if (flaglist == this.eventFlagList)
        {
            this.eventFlags = flags.ToArray();
        }
        if (flaglist == this.clearFlagList)
        {
            this.clearFlags = flags.ToArray();
        }
    }

    public bool GetFlagByName(string flagName)
    {
        var flags = new List<string>(eventFlags);
        return flags.Contains(flagName);
    }

    public bool CheckItemName(string itemName)
    {
        foreach(var itemData in itemList)
        {
            if (itemData.itemName == itemName)
            {
                return true;
            }
        }

        return false;
    }

    public int GetItemIndex(string itemName)
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i].itemName == itemName)
            {
                return i;
            }
        }

        return -1;
    }
}

[Serializable]
public class FlagData
{
    public string flagName;
    public bool flag;

    public FlagData(string flagName, bool flag)
    {
        flagName = this.flagName;
        flag = this.flag;
    }
}

[Serializable]
public class BGMData
{
    [SerializeField]
    public AudioClip BGM;
    public string flagName;

    public BGMData(AudioClip BGM, string flagName)
    {
        BGM = this.BGM;
        flagName = this.flagName;
    }
}

[Serializable]
public class SoundEffectData
{
    [SerializeField]
    public AudioClip soundEffect;
    public string soundName;

    public SoundEffectData(AudioClip soundEffect, string soundName)
    {
        soundEffect = this.soundEffect;
        soundName = this.soundName;
    }
}

[Serializable]
public class ItemData
{
    public string itemName;

    [SerializeField]
    public Sprite itemImage;

    public string itemText;

    public ItemData(string itemName, Sprite itemImage, string itemText)
    {
        this.itemName = itemName;
        this.itemImage = itemImage;
        this.itemText = itemText;
    }
}