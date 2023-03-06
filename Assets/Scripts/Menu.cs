using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private StageManager stagemanager;

    private Image blinderPanel;

    public void Start()
    {
        stagemanager = GameObject.Find("StageManager").GetComponent<StageManager>();

        blinderPanel = GameObject.Find("BlinderPanel").GetComponent<Image>();
    }

    public void OnClickOpenMenu()
    {
        this.GetComponent<Image>().enabled = true;

        blinderPanel.enabled = true;
        blinderPanel.color = new Color (0, 0, 0, 0);

        foreach (Transform child in this.gameObject.transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    public void OnClickRestartStage()
    {
        this.GetComponent<Image>().enabled = false;

        blinderPanel.enabled = false;

        foreach (Transform child in this.gameObject.transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    public void OnClickOpenOption()
    {
        Debug.Log("オプションを設定できます。");
    }

    public void OnClickSaveStage()
    {
        Debug.Log("セーブします。");
    }

    public void OnClickBackHome()
    {
        Debug.Log("ホームに戻ります。");
    }
}
