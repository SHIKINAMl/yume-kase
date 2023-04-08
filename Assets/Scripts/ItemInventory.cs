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

    public bool notDisplayItemWindow = false;

    private string[] currentItemList;
    private string[] previousItemList;

    public void Start()
    {
        getitemwindow = GameObject.Find("GetItemWindow").GetComponent<GetItemWindow>();
        stagemanager = GameObject.Find("StageManager").GetComponent<StageManager>();

        previousItemList = (from i in stagemanager.itemList select i.itemName).ToArray();

        StartCoroutine(DisplayItem());
    }

    public void Update()
    {
        currentItemList = (from i in stagemanager.itemList select i.itemName).ToArray();


        if (!currentItemList.SequenceEqual(previousItemList))
        {
            StartCoroutine(DisplayItem());
        }

        previousItemList = currentItemList;
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
            child.localPosition = new Vector3(((-parent.sizeDelta.x/2) + ((600 + (1200*(float)i))/(float)stagemanager.inventorySize)), 0, 0);
        }

        int n = 0;

        foreach (Transform child in this.transform)
        {
            GameObject childObject = child.transform.GetChild(0).gameObject;
            Image childBackImage = child.GetComponent<Image>();
            Image childIconImage = childObject.GetComponent<Image>();
            
            if (n < stagemanager.itemList.Count)
            {
                childBackImage.color = new Color(1, 1, 1, 1f);
                childIconImage.enabled = true;
                childObject.GetComponent<ItemIcon>().itemName = stagemanager.itemList[n].itemName;
                childIconImage.sprite = stagemanager.itemList[n].itemImage;
            }
            else
            {
                childBackImage.color = new Color(0.9f, 0.9f, 0.9f, 1f);
                childIconImage.enabled = false;
            }

            n++;
        }
    }

    public void ChackEvents(GameObject item1, GameObject item2)
    {
        foreach (var i in inventoryEvents)
        {
            if ((i.combinationItem.materialItemName1 == item1.GetComponent<ItemIcon>().itemName && i.combinationItem.materialItemName2 == item2.GetComponent<ItemIcon>().itemName) ||
                (i.combinationItem.materialItemName1 == item2.GetComponent<ItemIcon>().itemName && i.combinationItem.materialItemName2 == item1.GetComponent<ItemIcon>().itemName))
            {
                stagemanager.itemList.RemoveAt(stagemanager.GetItemIndex(item1.GetComponent<ItemIcon>().itemName));
                stagemanager.itemList.RemoveAt(stagemanager.GetItemIndex(item2.GetComponent<ItemIcon>().itemName));

                if (!notDisplayItemWindow)
                {
                    getitemwindow.DisplayGetItem(i.combinationItem.itemName, i.combinationItem.itemImage);  
                }

                stagemanager.itemList.Add(new ItemData(i.combinationItem.itemName, i.combinationItem.itemImage, i.combinationItem.itemText));
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
    public CombinationItemData combinationItem = new CombinationItemData();
}

[Serializable]
public class CombinationItemData
{
    public string materialItemName1;
    public string materialItemName2;

    public string itemName;

    [SerializeField]
    public Sprite itemImage;

    public string itemText;
}