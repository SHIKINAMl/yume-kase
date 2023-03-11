using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchoolCameraMove : CameraMove
{
    public new void OnClickToLeft()
    {
        Vector3 pos = this.transform.position;


        if (pos.x == 0)
        {
            pos.x = 20;
        }

        else
        {
            pos.x -= 20;
        }

        this.transform.position = pos;
    }

    public new void OnClickToRight()
    {
        Vector3 pos = this.transform.position;

        if (pos.x == 20)
        {
            pos.x = 0;
        }

        else
        {
            pos.x += 20;
        }

        this.transform.position = pos;
    }
}
