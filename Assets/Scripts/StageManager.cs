using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class StageManager : MonoBehaviour
{
    [SerializeField]
    public int numberOfRoom = 1;

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
    public Dictionary<string, Sprite> itemList = new Dictionary<string, Sprite>();

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

    public bool GetFlagByName(string flagName)
    {
        foreach(var flag in eventFlagList)
        {
            if (flag.flagName == flagName)
            {
                return flag.flag;
            }
        }

        return false;
    }

    public void SetFlagByName(FlagData[] flaglist, string flagName, bool boolean)
    {
        foreach(var flag in flaglist)
        {
            if (flag.flagName == flagName)
            {
                flag.SetFlag(boolean);
            }
        }
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

    public void SetFlag(bool boolean)
    {
        if (!flag)
        {
            flag = boolean;
        }
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