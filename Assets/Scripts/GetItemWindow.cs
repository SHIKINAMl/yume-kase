using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetItemWindow : MonoBehaviour
{
    private Image image;

    public void Start()
    {
        image = GetComponent<Image>();
    }

    public void GetItem(string itemName, Sprite itemImage)
    {
        Time.timeScale = 0;

        image.enabled = true;

        foreach (Transform child in this.gameObject.transform)
        {
            child.gameObject.SetActive(true);
        }

        transform.Find("ItemImage").GetComponent<Image>().sprite = itemImage;
        transform.Find("ItemText").GetComponent<Text>().text = itemName + "を入手した。";
    }

    public void OnClickClocsWindow()
    {
        Time.timeScale = 1;

        image.enabled = false;

        foreach (Transform child in this.gameObject.transform)
        {
            child.gameObject.SetActive(false);
        }
    }
}
