using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshiLoop : MonoBehaviour
{
    SpriteRenderer guro;
    StageManager stageManager;

    bool isFade = false;
    bool isFadein = true;

    void Start()
    {
        guro = this.transform.Find("グロ飯").GetComponent<SpriteRenderer>();
        stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();

        guro.color = new Color (1, 1, 1, 0);
    
        StartCoroutine(loop());
    }

    void FixedUpdate()
    {
        try
        {
            bulkFade();
        }

        catch{}
    }

    private IEnumerator loop()
    {

        if (!stageManager.GetFlagByName("食った"))
        {
            yield return new WaitForSeconds(5);
            isFadein = true;
            isFade = true;
            yield return new WaitForSeconds(1);
            isFadein = false;
            isFade = true;

            StartCoroutine(loop());
        }
    }

    private void bulkFade()
    {
        if (isFade)
        {
            if (isFadein)
            {
                guro.color += new Color (0, 0, 0, 0.02f);

                if (guro.color.a > 0.5f)
                {
                    isFade = false;
                }
            }

            else
            {
                guro.color -= new Color (0, 0, 0, 0.02f);

                if (guro.color.a < 0f)
                {
                    isFade = false;
                }
            }
        }
    }
}
