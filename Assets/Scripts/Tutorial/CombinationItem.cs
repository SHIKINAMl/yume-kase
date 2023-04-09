using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CombinationItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
{
    public string itemName;

    private TutorialManager tutorialManager;

    private StageManager stageManager;
    private ItemInventory itemInventory;

    private Vector2 initialPosition;
    private RectTransform thisTransform;
    private RectTransform parentTransform;

    private int clickCount = 0;

    public bool isOnDrag = false;

    public void Start()
    {
        tutorialManager = GameObject.Find("TutorialManager").GetComponent<TutorialManager>();

        stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
        itemInventory = GameObject.Find("ItemInventory").GetComponent<ItemInventory>();
        thisTransform = this.GetComponent<RectTransform>();
        parentTransform = thisTransform.parent as RectTransform;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        initialPosition = thisTransform.anchoredPosition;
        tutorialManager.isOnDrag = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        thisTransform.position = eventData.position;
        thisTransform.SetParent(GameObject.Find("Canvas").transform);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        thisTransform.SetParent(parentTransform);
        thisTransform.anchoredPosition = initialPosition;
        tutorialManager.isOnDrag = false;
    }

    public void OnDrop(PointerEventData eventData)
    {
        var raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raycastResults);

        foreach (var hit in raycastResults)
        {
            if (hit.gameObject.CompareTag("ItemIcon") && hit.gameObject.transform.parent.name == "ItemWindow (Blind)")
            {
                stageManager.itemList.RemoveAt(stageManager.GetItemIndex("マイナスドライバー"));
                    stageManager.itemList.RemoveAt(stageManager.GetItemIndex("ドライバーグリップ"));

                    stageManager.itemList.Add(new ItemData 
                    (
                        itemInventory.inventoryEvents[0].combinationItem.itemName, 
                        itemInventory.inventoryEvents[0].combinationItem.itemImage, 
                        itemInventory.inventoryEvents[0].combinationItem.itemText
                    ));

                Transform guideUIs = GameObject.Find("GuideUIs").transform;
                guideUIs.GetComponent<Image>().enabled = false;

                for (int i = 13; i < 16; i++)
                {
                    Destroy(guideUIs.GetChild(i).gameObject);
                }

                tutorialManager.DisplayGetItem();
                tutorialManager.clickCounter++;
                   
                GameObject.Find("ItemInventory").GetComponent<ItemInventory>().notDisplayItemWindow = false;
                GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
        
                foreach (GameObject item in items)
                {
                    item.GetComponent<Item>().notDisplayItemWindow = false;
                }
            }
        }
    }
}

