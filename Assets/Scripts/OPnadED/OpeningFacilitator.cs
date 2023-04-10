using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OpeningFacilitator : MonoBehaviour
{
    private bool isFading = true;
    private bool isAnimating = false;
    private bool isTexting = false;

    private bool isFadingIn = true;
    private bool isFadingOut = false;

    private Image blinderPanel;

    private AudioSource audioSource;

    private Text text;
    public string[] texts;
    private int numberOfCurrent = 0;
    private float timer = 0;
    private bool isOpening = false;
    private bool isClicked = false;

    public void Start()
    {
        text = GameObject.Find("Text").GetComponent<Text>();
        text.enabled = false;

        blinderPanel = GameObject.Find("BlinderPanel").GetComponent<Image>();

        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0;
    }

    public void FixedUpdate()
    {

    }

    public void Update()
    {
        if (isFading)
        {
            if (isFadingIn)
            {
                blinderPanel.color -= new Color (0, 0, 0, Time.unscaledDeltaTime/2);
                audioSource.volume += Time.unscaledDeltaTime/2;
            
            
                if (blinderPanel.color.a <= 0)
                {
                    Time.timeScale = 0;
                    blinderPanel.enabled = true;

                    isFading = false;
                    isAnimating = true;

                    isFadingOut = true;
                }
            }

            if (isFadingOut)
            {
                blinderPanel.color += new Color (0, 0, 0, Time.unscaledDeltaTime/4);
                audioSource.volume -= Time.unscaledDeltaTime/2;

                if (blinderPanel.color.a >= 1)
                {
                    SceneManager.LoadScene("Tutorial");
                }
            }
        }

        if (isAnimating)
        {
            if (isFadingOut)
            {
                blinderPanel.color += new Color (0, 0, 0, Time.unscaledDeltaTime/2);
    
                if (blinderPanel.color.a >= 0.5)
                {
                    isOpening = true;
                    text.enabled = true;

                    timer = 0;
                    numberOfCurrent = 0;
                    text.text = texts[0];
                    text.color = new Color (1, 1, 1, 0);

                    isAnimating = false;
                    isTexting = true;

                    isFadingOut = false;
                    isFadingIn = true;
                }
            }
        }

        if (isTexting)
        {
            timer += Time.unscaledDeltaTime;

            if (!isClicked && isOpening && Input.GetMouseButtonDown(0))
            {
                text.color = new Color (1, 1, 1, 1);
                isClicked = true;
                isFadingIn = false;
            }

            if ((texts != null && timer >= 2.5f) || (isOpening && Input.GetMouseButtonDown(0)))
            {
                isFadingOut = true;
            }

            if (isFadingIn)
            {
                text.color += new Color (0, 0, 0, Time.unscaledDeltaTime);

                if (text.color.a >= 1)
                {
                    isFadingIn = false;
                }
            }

            if (isFadingOut)
            {
                text.color -= new Color (0, 0, 0, Time.unscaledDeltaTime);

                if (text.color.a <= 0)
                {
                    isFadingOut = false;
                    isClicked = false;
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
            isOpening = false;

            text.enabled = false;

            isFading = true;
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