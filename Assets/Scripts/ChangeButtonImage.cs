using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeButtonImage : MonoBehaviour
{
    public Sprite normalImage;
    public Sprite hoverImage;

    private Image buttonImage;

    public void Start()
    {
        buttonImage = this.GetComponent<Image>();

        buttonImage.sprite = normalImage;
    }

    public void PointerEnter()
    {
        buttonImage.sprite = hoverImage;
    }

    public void PointerExit()
    {
        buttonImage.sprite = normalImage;
    }

    public void OnClickResetMenu()
    {
        buttonImage.sprite = normalImage;
    }
}
