using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ZoomWindow : MonoBehaviour, IPointerClickHandler
{
    
    private GameObject toLeftButton;
    private GameObject toRightButton;

    public void Start()
    {
        toLeftButton = GameObject.Find("ToLeftButton");
        toRightButton = GameObject.Find("ToRightButton");
    }
    
    public void OnPointerClick(PointerEventData pointer)
    {
        foreach (Transform item in this.transform.Find("Items"))
        {
            item.gameObject.SetActive(false);
        }

        this.transform.Find("BackGround").GetComponent<SpriteRenderer>().enabled = false;
        //this.transform.Find("Items").GetComponent<SpriteRenderer>().enabled = false;

        this.GetComponent<SpriteRenderer>().enabled = false;
        this.GetComponent<Collider2D>().enabled = false;
        
        try
        {
            toLeftButton.SetActive(true);
            toRightButton.SetActive(true);
        }

        catch
        {
            
        }
    }
}
