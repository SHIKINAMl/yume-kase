using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
    [SerializeField]
    bool debug;
    // Start is called before the first frame update
    void Start()
    {
        orderlength = flagsOrder.Count;
        for (int i=0; i<orderlength; i++){
            moveHistory.Add("blank");
        }
        stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (string flag in flagsOrder)
        {
            if (stageManager.GetFlagByName(flag))
            {
                if (moveHistory[moveHistory.Count-1] != flag){
                    moveHistory.Add(flag);
                }
                stageManager.SetFlagByName(stageManager.eventFlagList, flag, false);
            }
        }
        if (moveHistory.GetRange(orderlength-5, 5).SequenceEqual(flagsOrder) || debug) {
            stageManager.SetFlagByName(stageManager.eventFlagList, flagname, true);
        }
        else
        {
            stageManager.SetFlagByName(stageManager.eventFlagList, flagname, false);
        }
    }
}
