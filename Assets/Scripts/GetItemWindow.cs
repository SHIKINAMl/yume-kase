using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetItemWindow : MonoBehaviour
{
    private Image image;

    private Image blinderPanel;

    private bool isOpening = false;

    public void Start()
    {
        image = GetComponent<Image>();

        blinderPanel = GameObject.Find("BlinderPanel").GetComponent<Image>();
    }

    public void Update()
    {
        if (isOpening && Input.GetMouseButtonDown(0))
        {
            Time.timeScale = 1;

            isOpening = false;

            image.enabled = false;

            blinderPanel.enabled = false;

            foreach (Transform child in this.gameObject.transform)
            {
                child.gameObject.SetActive(false);
            }
        }
    }

    public void DisplayGetItem(string itemName, Sprite itemImage)
    {
        Time.timeScale = 0;

        isOpening = true;

        image.enabled = true;

        blinderPanel.enabled = true;
        blinderPanel.color = new Color (0, 0, 0, 0.2f);

        foreach (Transform child in this.gameObject.transform)
        {
            child.gameObject.SetActive(true);
        }

        transform.Find("ItemImage").GetComponent<Image>().sprite = itemImage;
        transform.Find("ItemText").GetComponent<Text>().text = itemName + "を入手した。";
    }
}
