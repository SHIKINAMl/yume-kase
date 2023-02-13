using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemIcon : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
{
    public string itemName;

    private ItemInventory iteminventory;

    private Vector2 initialPosition;
    private RectTransform thisTransform;
    private RectTransform parentTransform;

    public void Start()
    {
        iteminventory = GameObject.Find("ItemInventory").GetComponent<ItemInventory>();

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
}
