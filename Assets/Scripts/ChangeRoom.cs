using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChangeRoom : MonoBehaviour, IPointerClickHandler
{
    public int destinationRoom = 0;

    [SerializeField]
    public int numberOfRoom => GameObject.Find("StageManager").GetComponent<StageManager>().numberOfRoom;

    public bool isFlagType;
    public string flagName;

    private Camera camera;
    private StageManager stagemanager;

    public void Start()
    {
        camera = GameObject.Find("MainCamera").GetComponent<Camera>();
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
        camera.OnClickChangeRoom(destinationRoom);
    }
} 
