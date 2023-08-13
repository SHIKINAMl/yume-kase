using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SetDirection : MonoBehaviour
{
    private StageManager stageManager;
    [SerializeField]
    string flag;
    [SerializeField]
    int destinationRoom;
    [SerializeField]
    int destinationSide;
    private CameraMove cameramove;
    private bool isChanged = false;
    // Start is called before the first frame update
    void Start()
    {
        stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
        cameramove = GameObject.Find("MainCamera").GetComponent<CameraMove>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (stageManager.GetFlagByName(flag) && !isChanged){
            cameramove.OnClickChangeRoom(destinationRoom, destinationSide);
            isChanged = true;
        }
    }
}
