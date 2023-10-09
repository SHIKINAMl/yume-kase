using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    private StageManager stageManager;

    private bool isFirstTime = true;

    public int clickCounter = 0;
    private bool isToLeft;

    private Transform guideUIs;

    private TutorialDisplay tutorialText;
    private TutorialDisplay tutorialGetItem;

    public string paintingName;
    public Sprite paintingImage;
    public string paintingText; 

    private Vector3 initialPosition;
    private float timer;

    public bool isWaiting = false;
    public bool isOnDrag = false;

    private AudioSource audioSource;

    public void Start()
    {
        stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();

        guideUIs = GameObject.Find("GuideUIs").transform;

        tutorialText = guideUIs.Find("TextWindow(Tutorial)").GetComponent<TutorialDisplay>();
        tutorialGetItem = guideUIs.Find("GetItemWindow(Tutorial)").GetComponent<TutorialDisplay>();

        GameObject.Find("ItemInventory").GetComponent<ItemInventory>().notDisplayItemWindow = true;
        GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
        
        foreach (GameObject item in items)
        {
            item.GetComponent<Item>().notDisplayItemWindow = true;
        }

        audioSource = GetComponent<AudioSource>();
    }

    public void Update()
    {
        if (stageManager.GetFlagByName("startStage"))
        {
            if (clickCounter == 0)
            {
                if (isFirstTime)
                {
                    guideUIs.GetComponent<Image>().enabled = true;

                    tutorialText.DisplayTexts(new string[] {"ここはどこだろう・・・？"});
                    
                    for (int i = 0; i < 4; i++)
                    {
                        AppearImage(i, true);
                    }

                    isFirstTime = false;
                }

                FlashImage(guideUIs.GetChild(0).gameObject, guideUIs.GetChild(1).transform.localPosition);
                FlashImage(guideUIs.GetChild(2).gameObject, guideUIs.GetChild(3).transform.localPosition);
            }

            if (clickCounter == 1 || clickCounter == 2)
            {
                if (isToLeft)
                {
                    if (isFirstTime)
                    {
                        AppearImage(2, false);
                        AppearImage(3, false);

                        isFirstTime = false;
                    }

                    FlashImage(guideUIs.GetChild(0).gameObject, guideUIs.GetChild(1).transform.localPosition);
                }

                else
                {
                    if (isFirstTime)
                    {
                        AppearImage(0, false);
                        AppearImage(1, false);

                        isFirstTime = false;
                    }

                    FlashImage(guideUIs.GetChild(2).gameObject, guideUIs.GetChild(3).transform.localPosition);
                }
            }

            if (clickCounter == 3)
            {
                if (isToLeft)
                {
                    if (isFirstTime)
                    {
                        AppearImage(2, true);
                        AppearImage(3, true);
                        AppearImage(0, false);
                        AppearImage(1, false);

                        tutorialText.DisplayTexts(new string[] {"どうやら自分の部屋のようだ。", "すこし探索してみよう。"});

                        isFirstTime = false;
                    }

                    FlashImage(guideUIs.GetChild(2).gameObject, guideUIs.GetChild(3).transform.localPosition);
                }

                else
                {
                    if (isFirstTime)
                    {
                        AppearImage(0, true);
                        AppearImage(1, true);
                        AppearImage(2, false);
                        AppearImage(3, false);

                        tutorialText.DisplayTexts(new string[] {"どうやら自分の部屋のようだ。", "すこし探索してみよう。"});

                        isFirstTime = false;
                    }

                    FlashImage(guideUIs.GetChild(0).gameObject, guideUIs.GetChild(1).transform.localPosition);
                }
            }

            if (clickCounter == 4)
            {
                if (isFirstTime)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        AppearImage(i, false);
                    }

                    AppearImage(4, true);
                    AppearImage(5, true);

                    tutorialText.DisplayTexts(new string[] {"これは何だろう・・・？", "見覚えがある気がするが・・・"});

                    isFirstTime = false;
                    isWaiting = true;
                }

                FlashImage(guideUIs.GetChild(4).gameObject, guideUIs.GetChild(5).transform.localPosition, 0.6f);
            }

            if (clickCounter == 5)
            {
                if (isFirstTime && !isWaiting)
                {
                    audioSource.Play();

                    AppearImage(6, true);
                    AppearImage(7, true);
                    AppearImage(8, true);

                    tutorialText.DisplayTexts(new string[] {"窓から出られるかも。"});

                    isFirstTime = false;
                }

                FlashImage(guideUIs.GetChild(6).gameObject, guideUIs.GetChild(7).transform.localPosition, 0.3f, 0, -40);
            }

            if (clickCounter == 6)
            {
                if (isFirstTime)
                {
                    AppearImage(9, true);
                    AppearImage(10, true);
                    AppearImage(11, true);

                    tutorialText.DisplayTexts(new string[] {"額縁？", "さっきの絵が入りそう。"});

                    isFirstTime = false;
                }

                FlashImage(guideUIs.GetChild(9).gameObject, guideUIs.GetChild(10).transform.localPosition, 0.3f, 0, -30);
            }

            if (clickCounter == 7)
            {
                if (isFirstTime)
                {
                    tutorialText.DisplayTexts(new string[] {"何も起こらないか・・・", "とりあえず絵を完成させてみよう。"});

                    GameObject.Find("ItemInventory").GetComponent<ItemInventory>().notDisplayItemWindow = false;
                    GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
        
                    foreach (GameObject item in items)
                    {
                        item.GetComponent<Item>().notDisplayItemWindow = false;
                    }

                    isFirstTime = false;
                    isWaiting = true;
                }

                if (stageManager.CheckItemName("ドライバーグリップ"))
                {
                    guideUIs.GetComponent<Image>().enabled = true;

                    GameObject.Find("ItemInventory").GetComponent<ItemInventory>().notDisplayItemWindow = true;
                    GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
    
                    foreach (GameObject item in items)
                    {
                        item.GetComponent<Item>().notDisplayItemWindow = true;
                    }
                    
                    DisplayGetItem(0, false);
                    DisplayGetItem(0);

                    clickCounter++;
                    isFirstTime = true;
                }
            }

            if (clickCounter == 8)
            {
                if (isFirstTime && !isWaiting)
                {
                    tutorialText.DisplayTexts(new string[] {"ドライバーとグリップ。", "くっつけられそうだ。"});

                    AppearImage(13, true);
                    AppearImage(14, true);
                    AppearImage(15, true);

                    guideUIs.GetChild(13).GetChild(0).GetComponent<Image>().enabled = true;
                    guideUIs.GetChild(14).GetChild(0).GetComponent<Image>().enabled = true;
                    guideUIs.GetChild(15).GetChild(0).GetComponent<Image>().enabled = true;
                    
                    guideUIs.GetChild(13).localPosition += new Vector3 (200*stageManager.GetItemIndex("マイナスドライバー"), 0, 0);
                    guideUIs.GetChild(14).localPosition += new Vector3 (200*stageManager.GetItemIndex("マイナスドライバー"), 0, 0);
                    guideUIs.GetChild(15).localPosition += new Vector3 (200*stageManager.GetItemIndex("マイナスドライバー"), 0, 0);

                    initialPosition = guideUIs.GetChild(14).GetChild(0).localPosition;

                    isFirstTime = false;

                    timer = 0;
                }

                guideUIs.GetChild(14).GetChild(0).localPosition += new Vector3 (-100*Time.deltaTime, (0.95f-timer)*100*Time.deltaTime, 0);

                timer += Time.deltaTime;

                if (guideUIs.GetChild(14).GetChild(0).localPosition.x <= initialPosition.x - 200 || isOnDrag)
                {
                    guideUIs.GetChild(14).GetChild(0).localPosition = initialPosition;
                    timer = 0;
                }

                if (isOnDrag)
                {
                    guideUIs.GetChild(14).GetChild(0).GetComponent<Image>().enabled = false;
                }

                else
                {
                    guideUIs.GetChild(14).GetChild(0).GetComponent<Image>().enabled = true;
                }
            }
        }
    }

    private void FlashImage(GameObject flashItem, Vector3 initialPosition, float magnification = 1, float adjustmentX = 0, float adjustmentY = 0)
    {
        flashItem.transform.localScale += new Vector3 (Time.deltaTime*magnification, Time.deltaTime*magnification, 0);
        flashItem.transform.localPosition += new Vector3 (Time.deltaTime*adjustmentX, Time.deltaTime*adjustmentY, 0);

        Image itemImage = flashItem.GetComponent<Image>();
        itemImage.color -= new Color (0, 0, 0, Time.deltaTime);

        if (itemImage.color.a <= 0)
        {
            flashItem.transform.localScale = new Vector3 (1, 1, 0);
            flashItem.transform.localPosition = initialPosition;
            itemImage.color = new Color (1, 1, 1, 1);
        }
    }

    private void AppearImage(int i, bool boolean)
    {
        guideUIs.GetChild(i).GetComponent<Image>().enabled = boolean;
    }

    public void DisplayGetItem(int i = 0, bool boolean = true)
    {
        //yield return null;
        
        string itemName = stageManager.itemList[stageManager.itemList.Count-(i+1)].itemName;
        Sprite itemImage = stageManager.itemList[stageManager.itemList.Count-(i+1)].itemImage;

        tutorialGetItem.DisplayGetItem(itemName, itemImage, boolean);
    }

    public void OnClickCount(bool isLeftButton)
    {
        clickCounter++;
        isToLeft = isLeftButton;
        isFirstTime = true;
    }

    public void OnClickGetPainting()
    {
        clickCounter++;

        stageManager.SetFlagByName(stageManager.eventFlagList, "GetPainting", true);
        stageManager.itemList.Add(new ItemData (paintingName, paintingImage, paintingText));

        //StartCoroutine(DisplayGetItem());
        DisplayGetItem();

        AppearImage(4, false);
        AppearImage(5, false);

        isFirstTime = true;
    }

    public void OnClickOpenCurtain()
    {
        clickCounter++;

        stageManager.SetFlagByName(stageManager.eventFlagList, "OpenCurtain", true);

        AppearImage(6, false);
        AppearImage(7, false);
        AppearImage(8, false);

        isFirstTime = true;
    }

    public void OnClickDecoratePainting()
    {
        clickCounter++;

        stageManager.SetFlagByName(stageManager.eventFlagList, "PaintingKey1", true);
        
        AppearImage(9, false);
        AppearImage(10, false);
        AppearImage(11, false);

        isFirstTime = true;

        stageManager.itemList = new List<ItemData>();

        guideUIs.GetComponent<Image>().enabled = false;
    }
}
