using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Zoom : MonoBehaviour, IPointerClickHandler
{
    public int destinationRoom = 0;

    public bool isFlagType;
    public string flagName;

    private bool isZoomIn = false;

    private Vector3 cameraPosition;
    private Vector3 buttonPosition;

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
        if (!isZoomIn)
        {        
            cameraPosition = GameObject.Find("MainCamera").transform.position;
            buttonPosition = this.transform.position;

            cameramove.OnClickZoomIn(destinationRoom);

            this.transform.position = new Vector3 ((destinationRoom-1)*20, -22, 0);

            //イメージの変更(UIが完成したら)

            isZoomIn = true;
        }

        else
        {
            cameramove.OnClickZoomOut(cameraPosition);

            this.transform.position = buttonPosition;

            isZoomIn = false;
        }
    }
}
