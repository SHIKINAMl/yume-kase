using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustScreenSize : MonoBehaviour
{
    private float currentScreenWidth;
    private float currentScreenHeight;

    public void Start()
    {
        currentScreenWidth = Screen.width;
        currentScreenHeight = Screen.height;
    }

    public void Update()
    {
        if (currentScreenWidth != Screen.width)
        {
            Screen.SetResolution(Screen.width, Screen.width*9/16, false);
            currentScreenWidth = Screen.width;
            currentScreenHeight = Screen.height;
        }

        if (currentScreenHeight != Screen.height)
        {
            Screen.SetResolution(Screen.height*16/9, Screen.height, false);
            currentScreenWidth = Screen.width;
            currentScreenHeight = Screen.height;
        }
    }
}
