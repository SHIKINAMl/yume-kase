using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemIcon : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler, IPointerClickHandler
{
    public string itemName;

    private StageManager stagemanager;
    private ItemInventory iteminventory;
    private ItemInfoWindow iteminfowindow;

    private Vector2 initialPosition;
    private RectTransform thisTransform;
    private RectTransform parentTransform;

    private int clickCount = 0;
    public float doubleClickInterval = 0.3f;

    public void Start()
    {
        stagemanager = GameObject.Find("StageManager").GetComponent<StageManager>();
        iteminventory = GameObject.Find("ItemInventory").GetComponent<ItemInventory>();
        iteminfowindow = GameObject.Find("ItemInfoWindow").GetComponent<ItemInfoWindow>();

        thisTransform = this.GetComponent<RectTransform>();
        parentTransform = thisTransform.parent as RectTransform;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        initialPosition = thisTransform.anchoredPosition;
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
    }

    public void OnDrop(PointerEventData eventData)
    {
        var raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raycastResults);

        foreach (var hit in raycastResults)
        {
            if (hit.gameObject.CompareTag("ItemIcon"))
            {
                iteminventory.ChackEvents(this.gameObject, hit.gameObject);
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        clickCount++;
        Invoke("DoubleClick", doubleClickInterval);
    }

    private void DoubleClick()
    {
        if (clickCount != 2) 
        {
            clickCount = 0; 
            return; 
        }     
        else
        { 
            clickCount = 0;
        }

        iteminfowindow.DisplayItemInfo(itemName, GetComponent<Image>().sprite, stagemanager.itemList[stagemanager.GetItemIndex(itemName)].itemText);
    }
}
