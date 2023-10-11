using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndingFacilitator : MonoBehaviour
{
    private bool isFadingPart = true;
    private bool isCutInPart = false;
    private bool isEndrollOPart = false;

    public List<Sprite> images = new List<Sprite>();

    public List<int> noDisappearNumber;
    private int counter = 0;

    private SpriteRenderer spriteRenderer;
    public GameObject prefab;
    private SpriteRenderer prefabSpriteRenderer;

    private int indexOfImages = 0;
    private float waitingTime = 0;

    private Image blinderPanel;
    private Text title;

    public string stageNumber;

    private float timer = 0f;


    private bool isWaiting = false;
    private bool isFadingOut = false;
    private bool isFadingIn = true;

    private int orderInLayer = 1;

    private void Start()
    {
        spriteRenderer = GameObject.Find("BackImage").GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = images[0];
        spriteRenderer.color = new Color (1, 1, 1, 0);

        blinderPanel = GameObject.Find("BlinderPanel").GetComponent<Image>();
        blinderPanel.enabled = false;
        blinderPanel.color = new Color (0, 0, 0, 0);

        title = GameObject.Find("Title").GetComponent<Text>();
        title.color = new Color (1, 1, 1, 0);
        string formatTitle = string.Join(' ', stageNumber.Split());
        title.text = $"第 {formatTitle} 夢";
    }

    public void Update()
    {
        if (isFadingPart)
        {
            if (isWaiting)
            {
                waitingTime += Time.deltaTime;

                if (waitingTime >= 1.2f)
                {
                    indexOfImages += 1;

                    if (images.Count == indexOfImages)
                    {
                        isFadingPart = false;
                        isCutInPart = true;  

                        blinderPanel.enabled = true;
                    }
                
                    else
                    {
                        waitingTime = 0;
                        isWaiting = false;
                        isFadingOut = true;
                    }
                }
            }

            if (isFadingOut)
            {
                if (indexOfImages == noDisappearNumber[counter])
                {
                    GameObject prefabObject = Instantiate(prefab);
                    prefabObject.transform.SetParent(GameObject.Find("BackImage").transform);
                    prefabSpriteRenderer = prefabObject.GetComponent<SpriteRenderer>();
                    prefabSpriteRenderer.sprite = images[indexOfImages];
                    prefabSpriteRenderer.color = new Color (1, 1, 1, 0);
                    orderInLayer += 1;
                    prefabSpriteRenderer.sortingOrder = orderInLayer;

                    isFadingOut = false;
                    isFadingIn = true; 
                }

                else
                {
                    spriteRenderer.color -= new Color (0, 0, 0, Time.unscaledDeltaTime);

                    foreach (Transform prefabImages in GameObject.Find("BackImage").transform)
                    {
                        prefabImages.GetComponent<SpriteRenderer>().color -= new Color (0, 0, 0, Time.unscaledDeltaTime);
                    }
            
                    if (spriteRenderer.color.a <= 0)
                    {
                        foreach (Transform prefabImages in GameObject.Find("BackImage").transform)
                        {
                            Destroy(prefabImages.gameObject);
                            orderInLayer = 1;
                        }

                        spriteRenderer.sprite = images[indexOfImages];
                        isFadingOut = false;
                        isFadingIn = true; 
                    }
                }
            }

            if (isFadingIn)
            {
                if (indexOfImages == noDisappearNumber[counter])
                {
                    prefabSpriteRenderer.color += new Color (0, 0, 0, Time.unscaledDeltaTime/2);

                    if (prefabSpriteRenderer.color.a >= 1)
                    {
                        isFadingIn = false;
                        isWaiting = true;
                        counter += 1;
                    }
                }

                else
                {
                    spriteRenderer.color += new Color (0, 0, 0, Time.unscaledDeltaTime/2);
            
                    if (spriteRenderer.color.a >= 1)
                    {
                        isFadingIn = false;
                        isWaiting = true;
                    }
                }

            }

            if (Input.GetMouseButtonDown(0))
            {
                blinderPanel.enabled = true;

                //isFadingPart = false;
                isCutInPart = true;
            }
        }

        if (isCutInPart)
        {
            if (blinderPanel.color.a < 0.5)
            {
                blinderPanel.color += new Color (0, 0, 0, Time.unscaledDeltaTime/4);
                title.color += new Color (0, 0, 0, Time.unscaledDeltaTime/3);
            }

            else 
            {
                timer += Time.unscaledDeltaTime;

                if (timer >= 1f)
                {
                    blinderPanel.color += new Color (0, 0, 0, Time.unscaledDeltaTime/4);
                    title.color -= new Color (0, 0, 0, Time.unscaledDeltaTime/3);
                }
            }

            if (blinderPanel.color.a >= 1)
            {
                isCutInPart = false;
                isEndrollOPart = true;

            }
        }

        if (isEndrollOPart)
        {
            SceneManager.LoadScene("ED");
        }
    }
}
