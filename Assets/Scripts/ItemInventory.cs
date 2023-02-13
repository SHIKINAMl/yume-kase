using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

public class ItemInventory : MonoBehaviour
{
    private StageManager stagemanager;

    public GameObject itemWindow;

    public void Start()
    {
        stagemanager = GameObject.Find("StageManager").GetComponent<StageManager>();

        DisplayItem();
    }

    public void DisplayItem()
    {
        RectTransform parent = this.GetComponent<RectTransform>();

        for (int i = 0; i < stagemanager.inventorySize; i++)
        {
            RectTransform child = Instantiate(itemWindow).GetComponent<RectTransform>();
            child.SetParent(parent);
            child.localPosition = new Vector3(-parent.sizeDelta.x/2 + (150 + 300*(float)i)/(float)stagemanager.inventorySize , 0, 0);
        }

        int n = 0;

        foreach (Transform child in this.GetComponentInChildren<Transform>())
        {
            GameObject childObject = child.transform.GetChild(0).gameObject;
            Image childBackImage = child.GetComponent<Image>();
            Image childIconImage = childObject.GetComponent<Image>();
            
            if (n < stagemanager.itemList.Count)
            {
                childBackImage.color = new Color(0.9f, 0.9f, 0.9f, 1f);
                childIconImage.enabled = true;
                childIconImage.sprite = stagemanager.itemList.Values.ToArray()[n];
            }
            else
            {
                childBackImage.color = new Color(0.8f, 0.8f, 0.8f, 1f);
                childIconImage.enabled = false;
            }

            n++;
        }
    }
}
