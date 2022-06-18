using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SourceManager : MonoBehaviour
{
    static SourceManager inst;
    public static bool Ready;
    
    public static Texture Left => inst.leftCameraTexture;
    public static Texture Right => inst.rightCameraTexture;

    public WebCamTexture leftCameraTexture;
    public WebCamTexture rightCameraTexture;


    public string leftCameraName;
    public string rightCameraName;

    public Vector2Int resolution;
    public int fps;

    private void Awake()
    {
        inst = this;
    }
    private IEnumerator Start()
    {
        foreach(var d in WebCamTexture.devices)
        {
            Debug.Log(d.name);
        }

        leftCameraTexture = new WebCamTexture(leftCameraName, resolution.x, resolution.y, fps);
        leftCameraTexture.Play();
        yield return new WaitForSeconds(1f);
        rightCameraTexture = new WebCamTexture(rightCameraName, resolution.x, resolution.y, fps);
        rightCameraTexture.Play();
        yield return new WaitForSeconds(1f);

        Ready = true;
    }


}
