using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingFacilitator : MonoBehaviour
{
    private bool isFadingPart = true;
    private bool isCutInPart = false;
    private bool isEndrollOPart = false;

    [SerializeField]
    public List<Sprite> images = new List<Sprite>();

    private SpriteRenderer spriteRenderer;

    private int indexOfImages = 0;
    private float waitingTime = 0;

    private bool isWaiting = true;
    private bool isFadingOut = false;
    private bool isFadingIn = false;

    private void Start()
    {
        spriteRenderer = GameObject.Find("BackImage").GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = images[0];
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
                    waitingTime = 0;
                    isWaiting = false;
                    isFadingOut = true;
                }
            }

            if (isFadingOut)
            {
                spriteRenderer.color -= new Color (0, 0, 0, 0.002f);
            
                if (spriteRenderer.color.a <= 0)
                {
                    indexOfImages += 1;

                    if (images.Count == indexOfImages)
                    {
                        isFadingPart = false;
                        isCutInPart = true;
                    }
                    
                    else
                    {
                        spriteRenderer.sprite = images[indexOfImages];
                        isFadingOut = false;
                        isFadingIn = true; 
                    }

                }
            }

            if (isFadingIn)
            {
                spriteRenderer.color += new Color (0, 0, 0, 0.002f);
            
                if (spriteRenderer.color.a >= 1)
                {
                    isFadingIn = false;
                    isWaiting = true;
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
