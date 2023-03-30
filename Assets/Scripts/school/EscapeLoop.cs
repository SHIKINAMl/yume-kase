using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeLoop : MonoBehaviour
{
    StageManager stageManager;
    [SerializeField]
    List<string> flagsOrder = new List<string>();
    [SerializeField]
    List<string> moveHistory = new List<string>();
    [SerializeField]
    string flagname;
    int orderlength; 
    // Start is called before the first frame update
    void Start()
    {
        orderlength = flagsOrder.Count;
        stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (string flag in flagsOrder)
        {
            if (stageManager.GetFlagByName(flag))
            {
                moveHistory.Add(flag);
                stageManager.SetFlagByName(stageManager.eventFlagList, flag, false);
            }
        }
        if (moveHistory.Count >= 5 && moveHistory.GetRange(orderlength-5, 5) == flagsOrder) {
            stageManager.SetFlagByName(stageManager.eventFlagList, flagname, true);
        }
        else
        {
            stageManager.SetFlagByName(stageManager.eventFlagList, flagname, false);
        }
    }
}
