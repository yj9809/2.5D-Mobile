using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResolution : MonoBehaviour
{
    private void Awake()
    {
        Camera camera = GetComponent<Camera>();
        Rect rect = camera.rect;

        double scaleheight = ((float)Screen.width / Screen.height) / ((float)9 / 19.5);
        double scalewidth = 1f / scaleheight;

        if(scaleheight < 1)
        {
            rect.height = (float)scaleheight;
            rect.y = (float)(1f - scaleheight) / 2f;
        }
        else
        {
            rect.width = (float)scalewidth;
            rect.x = (float)(1f - scalewidth) / 2f;
        }

        camera.rect = rect;
    }

    private void OnPreCull()
    {
        GL.Clear(true, true, Color.black);
    }
}
