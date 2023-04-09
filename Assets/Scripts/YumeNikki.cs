using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class YumeNikki : MonoBehaviour
{
    private StageManager stageManager;

    private bool isOnStage = true;
    private bool isOnNikkiAnime = false;
    private bool isOnNikkiText = false;

    private bool isFadingIn = true;
    private bool isFadingOut = false;

    private bool isWaiting = false;
    private bool isAnimating = false;

    private Image blinderPanel;
    private Text title;

    public string stageNumber;

    private string nextStageName;

    private SpriteRenderer NikkiPage;
    private Text NikkiText;
    private float timer = 0;
    private int counter = 0;
    private bool isClicked = false;

    public Sprite[] PageImages;
    public string[] texts;

    public void Start()
    {
        stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();

        blinderPanel = GameObject.Find("BlinderPanel").GetComponent<Image>();
        blinderPanel.enabled = true;
        blinderPanel.color = new Color (0, 0, 0, 1);

        title = GameObject.Find("Title").GetComponent<Text>();
        title.color = new Color (1, 1, 1, 0);
        string formatTitle = string.Join(' ', stageNumber.Split());
        title.text = $"第 {formatTitle} 夢";
        
        NikkiPage = transform.Find("Page").GetComponent<SpriteRenderer>();
        NikkiText = GameObject.Find("NikkiText").GetComponent<Text>();

        //Time.timeScale = 0;
    }

    public void Update()
    {
        if (isOnStage)
        {
            if (isWaiting)
            {
                foreach (var flagName in stageManager.clearFlags)
                {  
                    GameObject.Find("SaveManager").GetComponent<SaveManager>().OnSave(flagName, gameObject.scene.name, true);
                    nextStageName = flagName;

                    blinderPanel.enabled = true;
                    blinderPanel.color = new Color (0, 0, 0, 0);

                    isWaiting = false;
                    isFadingOut = true;
                }
            }

            if (isFadingIn)
            {
                if (blinderPanel.color.a > 0.5)
                {
                    blinderPanel.color -= new Color (0, 0, 0, Time.unscaledDeltaTime/6);
                    title.color += new Color (0, 0, 0, Time.unscaledDeltaTime/6);
                }

                else 
                {
                    title.color -= new Color (0, 0, 0, Time.unscaledDeltaTime/4);

                    timer += Time.unscaledDeltaTime;

                    if (timer >= 1f)
                    {
                        blinderPanel.color -= new Color (0, 0, 0, Time.unscaledDeltaTime/4);
                    }
                }

                if (blinderPanel.color.a <= 0)
                {
                    //Time.timeScale = 1;
                    timer = 0;

                    blinderPanel.enabled = false;
                    title.enabled = false;
                    isFadingIn = false;
                    isWaiting = true;

                    stageManager.SetFlagByName(stageManager.eventFlagList, "startStage", true);
                }
            }

            if (isFadingOut)
            {
                blinderPanel.color += new Color (0, 0, 0, Time.unscaledDeltaTime/2);

                if (blinderPanel.color.a >= 1)
                {
                    Transform canvas = GameObject.Find("Canvas").transform;

                    foreach (Transform UIObject in canvas)
                    {
                        if (UIObject.gameObject.name != "NikkiText" && UIObject.gameObject.name != "BlinderPanel")
                        {
                            Destroy(UIObject.gameObject);
                        }
                    }

                    GameObject.Find("MainCamera").GetComponent<CameraMove>().MoveToNikki();

                    isOnStage = false;
                    isOnNikkiAnime = true;

                    isFadingOut = false;
                    isFadingIn = true;
                }
            }
        }

        else if (isOnNikkiAnime)
        {
            if (isFadingIn)
            {
                blinderPanel.color -= new Color (0, 0, 0, Time.unscaledDeltaTime/2);
            
                if (blinderPanel.color.a <= 0)
                {
                    blinderPanel.enabled = false;
                    isFadingIn = false;
                    isAnimating = true;
                }
            }

            if (isAnimating)
            {
                timer += Time.unscaledDeltaTime;

                if (timer >= 0.05f)
                {
                    NikkiPage.sprite = PageImages[counter];

                    timer = 0;
                    counter += 1;

                    if (PageImages.Length == counter)
                    {
                        NikkiPage.enabled = false;
                        blinderPanel.enabled = true;
                        isAnimating = false;
                        isFadingOut = true;
                    }
                }
            }

            if (isFadingOut)
            {
                blinderPanel.color += new Color (0, 0, 0, Time.unscaledDeltaTime/2);
            
                if (blinderPanel.color.a >= 0.5f)
                {
                    timer = 0;
                    counter = 0;
                    NikkiText.enabled = true;            
                    NikkiText.text = texts[0];
                    NikkiText.color = new Color (1, 1, 1, 0);

                    isFadingOut = false;
                    isOnNikkiAnime = false;

                    isFadingIn = true;
                    isOnNikkiText = true;
                }
            }
        }

        else if (isOnNikkiText)
        {
            timer += Time.unscaledDeltaTime;

            if (!isClicked && Input.GetMouseButtonDown(0))
            {
                NikkiText.color = new Color (1, 1, 1, 1);
                isClicked = true;
                isFadingIn = false;
            }

            if ((texts != null && timer >= 2) || Input.GetMouseButtonDown(0))
            {
                isFadingOut = true;
            }  

            if (isFadingIn)
            {                    
                NikkiText.color += new Color (0, 0, 0, Time.unscaledDeltaTime);

                if (NikkiText.color.a >= 1)
                {
                    isFadingIn = false;
                }
            }

            if (isFadingOut)
            {

                NikkiText.color -= new Color (0, 0, 0, Time.unscaledDeltaTime);

                if (NikkiText.color.a <= 0)
                {
                    isFadingOut = false;
                    isClicked = false;
                    DisplayTexts();
                }
            }
        }

        else
        {
            if (isFadingOut)
            {
                blinderPanel.color += new Color (0, 0, 0, Time.unscaledDeltaTime/2);

                if (blinderPanel.color.a >= 1)
                {
                    SceneManager.LoadScene(nextStageName);
                }
            }
        }
    }

    private void DisplayTexts()
    {
        timer = 0;
        counter++; 

        if (texts.Length == counter)
        {
            NikkiText.enabled = false;

            isOnNikkiText = false;
            isFadingOut = true;
        }
        else
        {
            NikkiText.text = texts[counter];
            NikkiText.color = new Color (1, 1, 1, 0);
            isFadingIn = true;
        }
    }
}
