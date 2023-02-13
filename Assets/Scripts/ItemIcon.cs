using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemIcon : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Vector2 initialPosition;
    private RectTransform thisTransform;
    private RectTransform parentTransform;

    public void Start()
    {
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
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        thisTransform.anchoredPosition = initialPosition;
    }

    /*
    private Vector2 GetLocalPosition(Vector2 screenPosition)
    {
        Vector2 result = Vector2.zero;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentTransform, screenPosition, Camera.main, out result);

        return result;
    }
    */
}
