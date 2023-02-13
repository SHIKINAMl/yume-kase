using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ItemInventory : MonoBehaviour
{
    private StageManager stagemanager;

    public GameObject itemWindow;

    public void Start()
    {
        stagemanager = GameObject.Find("StageManager").GetComponent<StageManager>();

        StartCoroutine(DisplayItem());
    }

    public IEnumerator DisplayItem()
    {
        RectTransform parent = this.GetComponent<RectTransform>();

        foreach (Transform child in this.transform)
        {
            Destroy(child.gameObject);
        }

        yield return null;

        for (int i = 0; i < stagemanager.inventorySize; i++)
        {
            RectTransform child = Instantiate(itemWindow).GetComponent<RectTransform>();
            child.SetParent(parent);
            child.localScale = new Vector3(1, 1, 1);
            child.localPosition = new Vector3(((-parent.sizeDelta.x/2) + ((300 + (600*(float)i))/(float)stagemanager.inventorySize)), 0, 0);
        }

        int n = 0;

        foreach (Transform child in this.transform)
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

        for(int i = 1; i <= this.transform.childCount; i++)
        {
            this.transform.GetChild(0).transform.SetSiblingIndex(this.transform.childCount - i);
        }

    }
}
