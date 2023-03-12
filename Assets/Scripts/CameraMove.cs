using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class CameraMove : MonoBehaviour
{
    private StageManager stagemanager;
    private BGMData[] BGMList;

    private bool[] currentFlagList;
    private bool[] lastTimeFlagList;

    public void Start()
    {
        stagemanager = GameObject.Find("StageManager").GetComponent<StageManager>();
        BGMList = stagemanager.BGMList;
        GetComponent<AudioSource>().PlayOneShot(BGMList[0].BGM);
    }

    public void Update()
    {
        for (var i = 1; i < BGMList.Length; i++)
        {
            if (lastTimeFlagList != currentFlagList)
            {
                GetComponent<AudioSource>().PlayOneShot(BGMList[i].BGM);
            }
        }

        lastTimeFlagList = currentFlagList;
        currentFlagList = (from i in BGMList select stagemanager.GetFlagByName(i.flagName)).ToArray();
    }

    public void OnClickToLeft()
    {
        Vector3 pos = this.transform.position;


        if (pos.x == 0)
        {
            pos.x = (stagemanager.sideList[(int)this.transform.position.y/20] - 1) * 20;
        }
        else
        {
            pos.x -= 20;
        }

        this.transform.position = pos;
    }

    public void OnClickToRight()
    {
        Vector3 pos = this.transform.position;

        if (pos.x == (stagemanager.sideList[(int)this.transform.position.y/20] - 1) * 20)
        {
            pos.x = 0;
        }
        else
        {
            pos.x += 20;
        }

        this.transform.position = pos;
    }

    public void OnClickChangeRoom(int roomNumber, int sideNumber)
    {
        this.transform.position = new Vector3 ((sideNumber-1)*20, (roomNumber-1)*20, -10);
    }

    public void OnClickZoomIn(int zoomChannel)
    {
        this.transform.position = new Vector3 ((zoomChannel-1)*20, -20, -10);
    }

    public void OnClickZoomOut(Vector3 cameraPosition)
    {
        this.transform.position = cameraPosition;
    }
}
