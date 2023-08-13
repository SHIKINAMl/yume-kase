 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class NurseAnimation : MonoBehaviour
{
    private Transform doorOpen;
    private StageManager stageManager;
    private bool startAnimation;
    [SerializeField]
    string flag;
    // Start is called before the first frame update
    void Start()
    {
        doorOpen = GameObject.Find("ドアオープン").GetComponent<Transform>();
        stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
        startAnimation = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (stageManager.GetFlagByName(flag)){
            startAnimation = true;
        }
        if(startAnimation){
            doorOpen.position += new Vector3(0.05f, 0, 0);
        }
    }
}
 
