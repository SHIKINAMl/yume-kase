using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private bool isWaiting = false;
    private bool isFadingOut = false;
    private bool isFadingIn = true;

    private void Start()
    {
        spriteRenderer = GameObject.Find("BackImage").GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = images[0];
        spriteRenderer.color = new Color (1, 1, 1, 0);
    }

    public void Update()
    {
        if (isFadingPart)
        {
            if (isWaiting)
            {
                waitingTime += Time.deltaTime;

                if (waitingTime >= 2)
                {
                    indexOfImages += 1;

                    if (images.Count == indexOfImages)
                    {
                        isFadingPart = false;
                        isCutInPart = true;  
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
                    isFadingOut = false;
                    isFadingIn = true; 
                }

                else
                {
                    spriteRenderer.color -= new Color (0, 0, 0, 0.002f);

                    foreach (Transform prefabImages in GameObject.Find("BackImage").transform)
                    {
                        prefabImages.GetComponent<SpriteRenderer>().color -= new Color (0, 0, 0, 0.002f);
                    }
            
                    if (spriteRenderer.color.a <= 0)
                    {
                        foreach (Transform prefabImages in GameObject.Find("BackImage").transform)
                        {
                            Destroy(prefabImages.gameObject);
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
                    prefabSpriteRenderer.color += new Color (0, 0, 0, 0.001f);

                    if (prefabSpriteRenderer.color.a >= 1)
                    {
                        isFadingIn = false;
                        isWaiting = true;
                        counter += 1;
                    }
                }

                else
                {
                    spriteRenderer.color += new Color (0, 0, 0, 0.001f);
            
                    if (spriteRenderer.color.a >= 1)
                    {
                        isFadingIn = false;
                        isWaiting = true;
                    }
                }

            }
        }

        if (isCutInPart)
        {
            Debug.Log("CutInPart");

            if (true)
            {
                isCutInPart = false;
                isEndrollOPart = true;
            }
        }

        if (isEndrollOPart)
        {
            Debug.Log("EndrollPart");

            if (true)
            {
                isEndrollOPart = false;
            }
        }
    }
}
