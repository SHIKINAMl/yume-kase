using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.IO;

public class HomeManager : MonoBehaviour
{
    private SaveManager saveManager;

    private SaveData saveData;

    private GameObject firstTime;
    private GameObject afterTime;

    private bool isFirstTime;

    private Image blinderPanel;

    private AudioSource audioSource;

    private bool isFadingOut = false;

    private string filePath;

    private bool toStart = false;
    private bool toReturn = false;

    public void Start()
    {
        saveManager = GameObject.Find("SaveManager").GetComponent<SaveManager>();
        saveData = saveManager.LoadSave();

        firstTime = GameObject.Find("FirstTime");
        afterTime = GameObject.Find("AfterTime");

        blinderPanel = GameObject.Find("BlinderPanel").GetComponent<Image>();
        blinderPanel.enabled = false;


        audioSource = GetComponent<AudioSource>();

        if (saveData.stageName == "None")
        {
            afterTime.SetActive(false);
            isFirstTime = true;
        }

        else
        {
            firstTime.SetActive(false);
            isFirstTime = false;
        }
    }

    public void Update()
    {
        if (isFadingOut)
        {
            blinderPanel.color += new Color (0, 0, 0, Time.unscaledDeltaTime/2);
            audioSource.volume -= Time.unscaledDeltaTime/2;

            if (blinderPanel.color.a >= 1)
            {
                if (toStart)
                {
                    saveManager.ClearSave();
                    SceneManager.LoadScene("OP");
                }

                else if (toReturn)
                {
                    if (saveData.stageName != "ED")
                    {
                        SceneManager.LoadScene(saveData.stageName);
                    }

                    else
                    {
                        SceneManager.LoadScene("Stage3");
                    }
                }
            }
        }
    }

    public void OnClickStart()
    {
        if (!isFirstTime)
        {
            Debug.Log("警告");
        }

        toStart = true;

        blinderPanel.enabled = true;
        isFadingOut = true;
    }

    public void OnClickReturn()
    {
        toReturn = true;

        blinderPanel.enabled = true;
        isFadingOut = true;
    }

    public void OnClickOpenOption()
    {
        Debug.Log("オプションを開く");
    }

    public void OnClickOpenCredit()
    {
        Debug.Log("クレジットを開く");
    }
}
