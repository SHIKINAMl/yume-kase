using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectStage : MonoBehaviour
{
    private HomeManager homeManager;
    private Image window;

    private Image stageImage;
    public List<Sprite> stageImages = new List<Sprite>();
    public List<string> stageNames = new List<string>();
    public int stageNumber = 0;

    void Start()
    {
        homeManager = GameObject.Find("HomeManager").GetComponent<HomeManager>();

        window = this.GetComponent<Image>();
        window.enabled = false;

        foreach (Transform child in this.transform)
        {
            child.gameObject.SetActive(false);
        }

        stageImage = transform.Find("SelectButton").GetComponent<Image>();

        stageNumber = stageNames.IndexOf(homeManager.saveData.stageName);
    }

    public void OnClickOpen()
    {
        window.enabled = true;

        foreach (Transform child in this.transform)
        {
            child.gameObject.SetActive(true);
        }

        stageImage.sprite = stageImages[stageNumber];
    }

    public void OnClickClose()
    {
        window.enabled = false;

        foreach (Transform child in this.transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    public void OnClickToStage()
    {
        stageNumber += 1;

        if (stageNumber > stageImages.Count - 1)
        {
            stageNumber = 0;
        }

        stageImage.sprite = stageImages[stageNumber];
    }

    public void OnClickBackStage()
    {
        stageNumber -= 1;

        if (stageNumber < 0)
        {
            stageNumber = stageImages.Count - 1;
        }
        
        stageImage.sprite = stageImages[stageNumber];
    }
}