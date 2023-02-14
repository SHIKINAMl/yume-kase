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

        foreach (Transform child in this.gameObject.transform)
        {
            child.GetComponent<Image>().enabled = true;
            child.GetComponent<Button>().enabled = true;
        }
    }

    public void OnClickRestartStage()
    {
        this.GetComponent<Image>().enabled = false;

        blinderPanel.enabled = false;

        foreach (Transform child in this.gameObject.transform)
        {
            child.GetComponent<Image>().enabled = false;
            child.GetComponent<Button>().enabled = false;
        }
    }

    public void OnClickRedoStage()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
