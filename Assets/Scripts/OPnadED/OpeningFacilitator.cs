using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningFacilitator : MonoBehaviour
{
    private bool isFadingIn = true;
    private bool isAnimating = false;
    private bool isTexting = false;
    private bool isFadingOut = false;

    private TextWindow textWindow;
    public string[] texts;

    public void Start()
    {
        textWindow = GameObject.Find("TextWindow").GetComponent<TextWindow>();
    }

    public void Update()
    {
        if (isFadingIn)
        {
            isFadingIn = false;
            isTexting = true;
        }

        if (isTexting)
        {
            textWindow.SetTexts(texts);
            isTexting = false;
            isFadingOut = true;
        }

        if (isFadingOut)
        {

        }
    }
}