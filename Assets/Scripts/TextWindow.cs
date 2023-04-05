using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextWindow : MonoBehaviour
{
    private Image image;
    private Text text;

    private Image blinderPanel;

    private string[] poppingUpTexts;
    private int numberOfCurrent = 0;
    private float timer = 0;
    private bool isOpening = false;

    private int numberOfCharacters;

    public void Start()
    {
        image = GetComponent<Image>();
        text = transform.Find("Text").GetComponent<Text>();

        blinderPanel = GameObject.Find("BlinderPanel").GetComponent<Image>();
    }

    public void Update()
    {
        timer += Time.unscaledDeltaTime;

        if ((poppingUpTexts != null && timer >= numberOfCharacters / 4) || (isOpening && Input.GetMouseButtonDown(0)))
        {
            DisplayTexts();
        }
    }

    public void SetTexts(string[] texts)
    {
        Time.timeScale = 0;

        isOpening = true;

        image.enabled = true;
        text.enabled = true;

        blinderPanel.enabled = true;
        blinderPanel.color = new Color (0, 0, 0, 0.2f);

        timer = 0;
        numberOfCurrent = 0;
        poppingUpTexts = texts;
        text.text = poppingUpTexts[0];
        numberOfCharacters = poppingUpTexts[0].Length;
    }

    private void DisplayTexts()
    {
        timer = 0;
        numberOfCurrent++; 

        if (poppingUpTexts.Length == numberOfCurrent)
        {
            Time.timeScale = 1;

            isOpening = false;

            image.enabled = false;
            text.enabled = false;

            blinderPanel.enabled = false;

            poppingUpTexts = null;
        }
        else
        {
            text.text = poppingUpTexts[numberOfCurrent];
            numberOfCharacters = poppingUpTexts[0].Length;
        }
    }
}
