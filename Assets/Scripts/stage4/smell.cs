using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smell : MonoBehaviour
{
    public string text;
    public float interval;
    private TextWindow textWindow; 
    private StageManager stageManager;
    // Start is called before the first frame update
    void Start()
    {
        textWindow = GameObject.Find("TextWindow").GetComponent<TextWindow>();
        stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
        StartCoroutine(smellwww());
    }

    // Update is called once per frame
    private IEnumerator smellwww()
    {
        if (!stageManager.GetFlagByName("換気扇1")){
            yield return new WaitForSeconds(interval);
            stageManager.SetFlagByName(stageManager.eventFlagList, "もや発生", true);
            StartCoroutine(smellwww());
        }
    }
}
