using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialDisplay : MonoBehaviour
{
    private TutorialManager tutorialManager;

    private int mode = 0;

    private Image image;

    private Image blinderPanel;
    
    private string[] texts;
    private int counter = 0;
    private float timer = 0;

    public bool isOpening = false;
    private bool isWaiting = true;

    private float numberOfCharacters;

    public void Start()
    {
        tutorialManager = GameObject.Find("TutorialManager").GetComponent<TutorialManager>();

        image = GetComponent<Image>();

        blinderPanel = GameObject.Find("BlinderPanel(Tutorial)").GetComponent<Image>();
    }
    
    public void Update()
    {
        timer += Time.unscaledDeltaTime;

        if ((texts != null && timer >= numberOfCharacters / 4) || (isOpening && Input.GetMouseButtonDown(0)))
        {
            timer = 0;
            counter++;

            if (mode == 0)
            {
                if (texts.Length == counter)
                {
                    isOpening = false;

                    image.enabled = false;
                    transform.Find("Text").GetComponent<Text>().enabled = false;

                    blinderPanel.enabled = false;

                    texts = null;
                }

                else
                {
                    transform.Find("Text").GetComponent<Text>().text = texts[counter];
                    numberOfCharacters = texts[counter].Length;
                }
            }

            else
            {
                isOpening = false;

                image.enabled = false;

                blinderPanel.enabled = false;

                foreach (Transform child in this.gameObject.transform)
                {
                    child.gameObject.SetActive(false);
                }

                if (isWaiting)
                {
                    tutorialManager.isWaiting = false;
                }
            }
        }
    }

    public void DisplayTexts(string[] strings)
    {
        mode = 0;

        image.enabled = true;
        transform.Find("Text").GetComponent<Text>().enabled = true;

        isOpening = true;
        
        blinderPanel.enabled = true;
        blinderPanel.color = new Color (0, 0, 0, 0.2f);

        timer = 0;
        counter = 0;
        texts = strings;
        transform.Find("Text").GetComponent<Text>().text = texts[0];
        numberOfCharacters = texts[0].Length;
    }

    public void DisplayGetItem(string itemName, Sprite itemImage, bool boolean)
    {
        mode = 1;
        isWaiting = boolean;

        image.enabled = true;

        isOpening = true;

        blinderPanel.enabled = true;
        blinderPanel.color = new Color (0, 0, 0, 0.2f);

        foreach (Transform child in this.gameObject.transform)
        {
            child.gameObject.SetActive(true);
        }

        timer = 0;
        counter = 0;

        transform.Find("ItemImage").GetComponent<Image>().sprite = itemImage;
        transform.Find("ItemText").GetComponent<Text>().text = itemName + "を入手した。";
    }
}
