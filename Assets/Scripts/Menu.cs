using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private StageManager stagemanager;

    private Image blinderPanel;

    private bool isFadingOut;

    private Image checkWindow;
    private bool isChecking = false;

    public void Start()
    {
        stagemanager = GameObject.Find("StageManager").GetComponent<StageManager>();

        blinderPanel = GameObject.Find("BlinderPanel").GetComponent<Image>();
        checkWindow = GameObject.Find("CheckWindow").GetComponent<Image>();
    }

    public void Update()
    {
        if (isFadingOut)
        {
            blinderPanel.color += new Color (0, 0, 0, Time.unscaledDeltaTime/2);
            GetComponent<Image>().color -= new Color (0, 0, 0, Time.unscaledDeltaTime/2);

            foreach (Transform button in this.transform)
            {
                button.GetComponent<Image>().color -= new Color (0, 0, 0, Time.unscaledDeltaTime/2);
            }

            if (blinderPanel.color.a >= 1)
            {
                SceneManager.LoadScene("Home");
            }
        }

        if (isChecking)
        {
            if(Input.GetMouseButtonDown(0))
            {
                checkWindow.GetComponent<Image>().enabled = false;
            }
        }
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
        GameObject.Find("SaveManager").GetComponent<SaveManager>().OnSave(gameObject.scene.name, gameObject.scene.name, false);

        checkWindow.GetComponent<Image>().enabled = true;
        isChecking = true;
    }

    public void OnClickBackHome()
    {
        isFadingOut = true;
    }
}
