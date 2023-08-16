using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moya : MonoBehaviour
{
    private CameraMove stageManager;
    // Start is called before the first frame update
    void Start()
    {
        stageManager = GameObject.Find("MainCamera").GetComponent<CameraMove>();
    }

    void FixedUpdate()
    {
        this.transform.position = new Vector3 (stageManager.transform.position.x, stageManager.transform.position.y, 0);
    }
}
