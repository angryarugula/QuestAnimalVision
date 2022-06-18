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

    public GameObject[] visionTypes;

    public GameObject hintRoot;

    int currentVisionType;

    private void Awake()
    {
        inst = this;
    }
    private IEnumerator Start()
    {
        Texture.streamingTextureForceLoadAll = true;

        foreach (var d in WebCamTexture.devices)
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

        SetVisionType(0);
    }

    private void Update()
    {
        if (!Ready)
            return;

        if (Input.GetKeyUp(KeyCode.Backslash))
        {
            SwapSources();
        }

        for (int i = 0; i < visionTypes.Length; i++)
        {
            if (Input.GetKeyUp(KeyCode.Alpha1 + i))
            {
                SetVisionType(i);
            }
        }

        if (Input.GetKeyUp(KeyCode.H))
            hintRoot.SetActive(!hintRoot.activeSelf);

    }

    void SetVisionType(int index)
    {
        foreach (var go in visionTypes)
            go.SetActive(false);

        visionTypes[index].SetActive(true);

        currentVisionType = index;
    }

    void SwapSources()
    {
        var a = leftCameraTexture;
        var b = rightCameraTexture;

        leftCameraTexture = b;
        rightCameraTexture = a;

        SetVisionType(currentVisionType);
    }

}
