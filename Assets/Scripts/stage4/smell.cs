using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smell : MonoBehaviour
{
    public string text;
    public float interval;
    private TextWindow textWindow; 
    private StageManager stageManager;
    private bool smellFlag;
    // Start is called before the first frame update
    void Start()
    {
        smellFlag = true;
        textWindow = GameObject.Find("TextWindow").GetComponent<TextWindow>();
        stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
        StartCoroutine(smellwww());
    }

    void FixedUpdate()
    {
        if (stageManager.GetFlagByName("換気扇1")){
            smellFlag = false;
        }
    }

    // Update is called once per frame
    private IEnumerator smellwww()
    {
        yield return new WaitForSeconds(interval);
        if (smellFlag){    
            stageManager.SetFlagByName(stageManager.eventFlagList, "もや発生", true);
            stageManager.SetFlagByName(stageManager.eventFlagList, "もや発生2", true);
            stageManager.SetFlagByName(stageManager.eventFlagList, "もや発生3", true);
            StartCoroutine(smellwww());
        }
    }
}
