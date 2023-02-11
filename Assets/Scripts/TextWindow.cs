using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextWindow : MonoBehaviour
{
    private Image image;
    private Button button;
    private Text text;

    private string[] poppingUpTexts;
    private int numberOfCurrent = 0;
    private float timer = 0;

    public void Start()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
        text = transform.Find("PopUpText").GetComponent<Text>();
    }

    public void FixedUpdate()
    {
        timer += Time.deltaTime;

        if (poppingUpTexts != null && timer >= 5)
        {
            IndicateTexts();
        }
    }

    public void SetTexts(string[] texts)
    {
        image.enabled = true;
        button.enabled = true;
        text.enabled = true;
        timer = 0;
        numberOfCurrent = 0;
        poppingUpTexts = texts;
        text.text = poppingUpTexts[0];
    }

    private void IndicateTexts()
    {
        timer = 0;
        numberOfCurrent++; 

        if (poppingUpTexts.Length == numberOfCurrent)
        {
            image.enabled = false;
            button.enabled = false;
            text.enabled = false;
            poppingUpTexts = null;
        }
        else
        {
            text.text = poppingUpTexts[numberOfCurrent];
        }
    }

    public void OnClickToNextText()
    {
        IndicateTexts();
    }
}
