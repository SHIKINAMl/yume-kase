using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{
    private int count;
    private int rightCount;
    private int leftCount;
    private StageManager stageManager;

    // Start is called before the first frame update
    void Start()
    {
        count = 1;
        stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        string countFlag = "セリフカウント";
        string serifFlag = "換気扇";
        string leftFlag = "左へ";
        string rightFlag = "右へ";
        if (stageManager.GetFlagByName(countFlag))
        {
            count ++;
            stageManager.SetFlagByName(stageManager.eventFlagList, countFlag, false);
            stageManager.SetFlagByName(stageManager.eventFlagList, $"{serifFlag}{count}", true);
            stageManager.SetFlagByName(stageManager.eventFlagList, $"{serifFlag}{count-1}", false);
        }
        if (stageManager.GetFlagByName(rightFlag))
        {
            rightCount ++;
            stageManager.SetFlagByName(stageManager.eventFlagList, rightFlag, false);
        }
        if (stageManager.GetFlagByName(leftFlag))
        {
            leftCount ++;
            stageManager.SetFlagByName(stageManager.eventFlagList, leftFlag, false);
        }
        if (rightCount+leftCount == 6){
            // 分岐処理
        }
    }
}
