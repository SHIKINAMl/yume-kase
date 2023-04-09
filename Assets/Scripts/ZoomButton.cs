using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ZoomButton : MonoBehaviour, IPointerClickHandler
{
    public GameObject zoomWindow;

    public bool isFlagType;
    public string flagName;

    private StageManager stagemanager;

    private GameObject toLeftButton;
    private GameObject toRightButton;

    public void Start()
    {
        stagemanager = GameObject.Find("StageManager").GetComponent<StageManager>();

        toLeftButton = GameObject.Find("ToLeftButton");
        toRightButton = GameObject.Find("ToRightButton");

        foreach (Transform item in zoomWindow.transform.Find("Items").transform)
        {
            item.gameObject.SetActive(false);
        }

        zoomWindow.transform.Find("BackGround").GetComponent<SpriteRenderer>().enabled = false;
        //zoomWindow.transform.Find("Items").GetComponent<SpriteRenderer>().enabled = false;
        
        zoomWindow.GetComponent<SpriteRenderer>().enabled = false;
        zoomWindow.GetComponent<Collider2D>().enabled = false;
        
        if (isFlagType)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
        }
    }

    public void FixedUpdate()
    {
        if (isFlagType && stagemanager.GetFlagByName(flagName))
        {
            GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<Collider2D>().enabled = true;
        }
    }

    public void OnPointerClick(PointerEventData pointer)
    {      
        foreach (Transform item in zoomWindow.transform.Find("Items"))
        {
            item.gameObject.SetActive(true);
        }

        zoomWindow.transform.Find("BackGround").GetComponent<SpriteRenderer>().enabled = true;
        //zoomWindow.transform.Find("Items").GetComponent<SpriteRenderer>().enabled = true;
        
        zoomWindow.GetComponent<SpriteRenderer>().enabled = true;
        zoomWindow.GetComponent<Collider2D>().enabled = true;

        toLeftButton.SetActive(false);
        toRightButton.SetActive(false);
    }
}
