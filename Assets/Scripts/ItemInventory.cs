using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

public class ItemInventory : MonoBehaviour
{
    [SerializeField]
    public InventoryEvent[] inventoryEvents;

    public int numberOfEvent;

    private GetItemWindow getitemwindow;
    private StageManager stagemanager;

    public GameObject itemWindow;

    public void Start()
    {
        getitemwindow = GameObject.Find("GetItemWindow").GetComponent<GetItemWindow>();
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

        Debug.Log(1);

        yield return null;

        Debug.Log(2);
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
                childObject.GetComponent<ItemIcon>().itemName = stagemanager.itemList.Keys.ToArray()[n];
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

    public void ChackEvents(GameObject item1, GameObject item2)
    {
        foreach (var i in inventoryEvents)
        {
            if ((i.combinationItemList[0].itemName == item1.GetComponent<ItemIcon>().itemName && i.combinationItemList[1].itemName == item2.GetComponent<ItemIcon>().itemName) ||
                (i.combinationItemList[0].itemName == item2.GetComponent<ItemIcon>().itemName && i.combinationItemList[1].itemName == item1.GetComponent<ItemIcon>().itemName))
            {
                stagemanager.itemList.Remove(item1.GetComponent<ItemIcon>().itemName);
                stagemanager.itemList.Remove(item2.GetComponent<ItemIcon>().itemName);
                stagemanager.itemList.Add(i.combinationItemList[2].itemName, i.combinationItemList[2].itemImage);
                getitemwindow.GetItem(i.combinationItemList[2].itemName, i.combinationItemList[2].itemImage);
                StartCoroutine(DisplayItem());
                i.eventTrigger = false;
            }
        }
    }
}

[Serializable]
public class InventoryEvent
{
    //インベントリ内のイベントが増える場合使用
    //public int EventType = 1;

    public bool eventTrigger = true;

    [SerializeField]
    public ItemData[] combinationItemList = new ItemData[3];
}

[Serializable]
public class ItemData
{
    public string itemName;

    [SerializeField]
    public Sprite itemImage;

    public ItemData(string itemName, Sprite itemImage)
    {
        itemName = this.itemName;
        itemImage = this.itemImage;
    }
}