using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChangeRoom : MonoBehaviour, IPointerClickHandler
{
    public int destinationRoom = 1;
    public int destinationSide = 1;

    [SerializeField]
    public int numberOfRoom => GameObject.Find("StageManager").GetComponent<StageManager>().numberOfRoom;
    
    [SerializeField]
    public int[] sideList => GameObject.Find("StageManager").GetComponent<StageManager>().sideList;

    public bool isFlagType;
    public string flagName;

    private CameraMove cameramove;
    private StageManager stagemanager;

    public void Start()
    {
        cameramove = GameObject.Find("MainCamera").GetComponent<CameraMove>();
        stagemanager = GameObject.Find("StageManager").GetComponent<StageManager>();
        
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
        cameramove.OnClickChangeRoom(destinationRoom, destinationSide);
    }
} 
