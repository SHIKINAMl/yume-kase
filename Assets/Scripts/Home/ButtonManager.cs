using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    private SaveManager saveManager;

    private SaveData saveData;

    private GameObject firstTime;
    private GameObject afterTime;

    private bool isFirstTime;

    public void Start()
    {
        saveManager = GameObject.Find("SaveManager").GetComponent<SaveManager>();

        firstTime = GameObject.Find("FirstTime");
        afterTime = GameObject.Find("AfterTime");

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

    public void OnClickStart()
    {
        if (!isFirstTime)
        {
            Debug.Log("警告");
        }

        Debug.Log("チュートリアル開始");
        //SceneManager.LoadScene("stage1");
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
