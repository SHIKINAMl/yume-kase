using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OpeningFacilitator : MonoBehaviour
{
    private bool isFadingIn = true;
    private bool isAnimating = false;
    private bool isTexting = false;
    private bool isFadingOut = false;

    private Image blinderPanel;

    private Text text;
    public string[] texts;
    private int numberOfCurrent = 0;
    private float timer = 0;
    private bool isOpening = false;

    public void Start()
    {
        text = GameObject.Find("Text").GetComponent<Text>();
        text.enabled = false;

        blinderPanel = GameObject.Find("BlinderPanel").GetComponent<Image>();
    }

    public void FixedUpdate()
    {
        if (isFadingIn)
        {
            blinderPanel.color -= new Color (0, 0, 0, 0.005f);
            
            if (blinderPanel.color.a <= 0)
            {
                isTexting = true;
            }
        }

        if (isTexting)
        {
            Time.timeScale = 0;

            isOpening = true;
            text.enabled = true;

            blinderPanel.enabled = true;
            blinderPanel.color = new Color (0, 0, 0, 0.2f);

            timer = 0;
            numberOfCurrent = 0;
            text.text = texts[0];
            text.color = new Color (1, 1, 1, 0);
        }

        if (isFadingOut)
        {
            blinderPanel.color += new Color (0, 0, 0, 0.005f);

            if (blinderPanel.color.a >= 1)
            {
                SceneManager.LoadScene("Stage1");
            }
        }
    }

    public void Update()
    {
        if (isTexting)
        {
            
            timer += Time.unscaledDeltaTime;

            if (isOpening && Input.GetMouseButtonDown(0))
            {
                text.color = new Color (1, 1, 1, 1);
                isFadingIn = false;
            }

            if ((texts != null && timer >= 4) || (isOpening && Input.GetMouseButtonDown(0)))
            {
                isFadingOut = true;
            }

            if (isFadingIn)
            {
                text.color += new Color (0, 0, 0, 0.002f);

                if (text.color.a >= 1)
                {
                    isFadingIn = false;
                }
            }

            if (isTexting && isFadingOut)
            {
                text.color -= new Color (0, 0, 0, 0.002f);

                if (text.color.a <= 0)
                {
                    isFadingOut = false;
                    DisplayTexts();
                }
            }
        }
    }

    private void DisplayTexts()
    {
        timer = 0;
        numberOfCurrent++; 

        if (texts.Length == numberOfCurrent)
        {
            Time.timeScale = 1;

            isOpening = false;

            text.enabled = false;

            isTexting = false;
            isFadingOut = true;
        }
        else
        {
            text.text = texts[numberOfCurrent];
            text.color = new Color (1, 1, 1, 0);
            isFadingIn = true;
        }
    }
}