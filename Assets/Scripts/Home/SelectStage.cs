using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectStage : MonoBehaviour
{
    private Image window;

    private bool isActive = false;

    private Image stageImage;
    public List<Sprite> stageImages = new List<Sprite>();
    public List<string> stageNames = new List<string>();
    public int stageNumber = 0;

    void Start()
    {
        window = this.GetComponent<Image>();
        window.enabled = false;

        foreach (Transform child in this.transform)
        {
            child.gameObject.SetActive(false);
        }

        isActive = false;

        stageImage = transform.Find("SelectButton").GetComponent<Image>();
    }

    public void OnClickOpen()
    {
        window.enabled = true;

        foreach (Transform child in this.transform)
        {
            child.gameObject.SetActive(true);
        }

        isActive = true;
    }

    public void OnClickClose()
    {
        window.enabled = false;

        foreach (Transform child in this.transform)
        {
            child.gameObject.SetActive(false);
        }

        isActive = false;
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