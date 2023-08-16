using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshiLoop : MonoBehaviour
{
    SpriteRenderer meshi;
    SpriteRenderer guro;
    StageManager stageManager;
    bool isFade;
    SpriteRenderer fadeout;
    SpriteRenderer fadein;
    // Start is called before the first frame update
    void Start()
    {
        isFade = false;
        meshi = this.GetComponent<SpriteRenderer>();
        guro = GameObject.Find("グロ飯").GetComponent<SpriteRenderer>();
        stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
        guro.color = new Color (1, 1, 1, 0);
        meshi.color = new Color (0, 0, 0, 0.5f);
        StartCoroutine(loop());
    }

    void FixedUpdate()
    {
        bulkFade();
    }

    private IEnumerator loop()
    {
        Debug.Log("loop");
        if (!stageManager.GetFlagByName("食った")){
            yield return new WaitForSeconds(5);
            fadein = guro;
            fadeout = meshi;
            isFade = true;
            yield return new WaitForSeconds(1);
            fadein = meshi;
            fadeout = guro;
            isFade = true;
            StartCoroutine(loop());
        }
    }

    private void bulkFade(){
        if (isFade)
        {
            fadein.color += new Color (0, 0, 0, 0.02f);
            fadeout.color -= new Color (0, 0, 0, 0.02f);
            if (fadein.color.a > 0.5f)
            {
                isFade = false;
            }
        }

    }
}
