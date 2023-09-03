using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopSound : MonoBehaviour
{
    private AudioSource audioSource;
    private StageManager stageManager;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GameObject.Find("窓").GetComponent<AudioSource>();
        stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (stageManager.GetFlagByName("風の音ストップ")){
            audioSource.Stop();
            stageManager.SetFlagByName(stageManager.eventFlagList, "風の音ストップ", false);
        }
    }
}
