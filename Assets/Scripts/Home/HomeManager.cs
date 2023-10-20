using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using System;
using System.IO;

public class HomeManager : MonoBehaviour
{
    private SaveManager saveManager;

    private SelectStage selectStage;

    public SaveData saveData;

    private GameObject firstTime;
    private GameObject afterTime;

    private bool isFirstTime;

    private Image blinderPanel;

    private AudioSource audioSource;

    private bool isFadingOut = false;

    private string filePath;

    private bool toStart = false;
    private bool toReturn = false;

    private GameObject video;

    private void Awake()
    {
        saveManager = GameObject.Find("SaveManager").GetComponent<SaveManager>();

        saveData = saveManager.LoadSave();
    }
    public void Start()
    {
        firstTime = GameObject.Find("FirstTime");
        afterTime = GameObject.Find("AfterTime");

        blinderPanel = GameObject.Find("BlinderPanel").GetComponent<Image>();
        blinderPanel.enabled = false;

        video = GameObject.Find("Credit");

        audioSource = GetComponent<AudioSource>();

        /*
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
        */

        afterTime.SetActive(false);
        isFirstTime = true;
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
                    selectStage = GameObject.Find("SelectStageWindow").GetComponent<SelectStage>();
                    SceneManager.LoadScene(selectStage.stageNames[selectStage.stageNumber]);
                    /*
                    if (saveData.stageName != "ED")
                    {
                        SceneManager.LoadScene(saveData.stageName);
                    }

                    else
                    {
                        SceneManager.LoadScene("Stage6");
                    }
                    */
                }
            }
        }

        if (video.GetComponent<VideoPlayer>().isPlaying)
        {
            if (Input.GetMouseButtonDown(0))
            {
                video.GetComponent<VideoPlayer>().playbackSpeed = 6;
            }

            if (Input.GetMouseButtonUp(0))
            {
                video.GetComponent<VideoPlayer>().playbackSpeed = 2;
            }

            if (video.GetComponent<SpriteRenderer>().color.a <= 1)
            {
                video.GetComponent<SpriteRenderer>().color += new Color (0, 0, 0, 0.005f);
            }

            foreach (Transform transform in GameObject.Find("FirstTime").transform)
            {
                transform.gameObject.SetActive(false);
            }
        }

        else
        {
            if (video.GetComponent<SpriteRenderer>().color.a >= 0)
            {
                video.GetComponent<SpriteRenderer>().color -= new Color (0, 0, 0, 0.005f);
            }

            foreach (Transform transform in GameObject.Find("FirstTime").transform)
            {
                transform.gameObject.SetActive(true);
            }
        }
    }

    public void OnClickStart()
    {
        /*
        if (!isFirstTime)
        {
            Debug.Log("警告");
        }
        */

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
        video.GetComponent<VideoPlayer>().Play();
    }
}
