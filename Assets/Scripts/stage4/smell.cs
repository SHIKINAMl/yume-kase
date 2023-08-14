using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smell : MonoBehaviour
{
    public string text;
    public float interval;
    private TextWindow textWindow; 
    // Start is called before the first frame update
    void Start()
    {
        textWindow = GameObject.Find("TextWindow").GetComponent<TextWindow>();
        StartCoroutine(smellwww());
    }

    // Update is called once per frame
    private IEnumerator smellwww()
    {
        yield return new WaitForSeconds(interval);
        textWindow.SetTexts(new string[]{text}); // なんか匂うぞwww
        StartCoroutine(smellwww());
    }
}
