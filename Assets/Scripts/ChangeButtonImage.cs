using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeButtonImage : MonoBehaviour
{
    public Sprite normalImage;
    public Sprite hoverImage;

    public Vector3 adjustPosition;

    private Image buttonImage;

    private Vector3 initialPosition;

    public void Start()
    {
        buttonImage = this.GetComponent<Image>();

        initialPosition = this.transform.position;

        buttonImage.sprite = normalImage;
    }

    public void PointerEnter()
    {
        buttonImage.sprite = hoverImage;
        this.transform.position += adjustPosition;
    }

    public void PointerExit()
    {
        buttonImage.sprite = normalImage;
        this.transform.position -= adjustPosition;
    }

    public void OnClickResetMenu()
    {
        buttonImage.sprite = normalImage;
        this.transform.position = initialPosition;
    }
}
