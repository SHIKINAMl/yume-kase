using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    private SaveManager saveManager;

    private SaveData saveData;

    private GameObject firstTime;
    private GameObject afterTime;

    private bool isFirstTime;

    private Image blinderPanel;

    private bool isFadingOut = false;

    public void Start()
    {
        saveManager = GameObject.Find("SaveManager").GetComponent<SaveManager>();

        firstTime = GameObject.Find("FirstTime");
        afterTime = GameObject.Find("AfterTime");

        blinderPanel = GameObject.Find("BlinderPanel").GetComponent<Image>();
        blinderPanel.enabled = false;

        saveData = saveManager.LoadSave();
        Debug.Log(saveData.stageName);

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
            blinderPanel.color += new Color (0, 0, 0, 0.0005f);

            if (blinderPanel.color.a >= 1)
            {
                SceneManager.LoadScene("OP");
            }
        }
    }

    public void OnClickStart()
    {
        if (!isFirstTime)
        {
            Debug.Log("警告");
        }

        blinderPanel.enabled = true;
        isFadingOut = true;
    }

    public void OnClickReturn()
    {
        SceneManager.LoadScene(saveData.stageName);
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
