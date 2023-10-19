using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
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

    public GameObject forwardInstRoot;
    public GameObject sideInstRoot;
    public GameObject freeInstRoot;

    int currentVisionType;

    private void Awake()
    {
        inst = this;
    }
    private IEnumerator Start()
    {
        if (Display.displays.Length > 1)
            Display.displays[1].Activate();

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

        
        if (Keyboard.current.backslashKey.wasPressedThisFrame)
        {
            SwapSources();
        }

        for (int i = 0; i < visionTypes.Length; i++)
        {
            
            
            if (Keyboard.current.GetChildControl<KeyControl>((i + 1).ToString()).wasPressedThisFrame)
            {
                SetVisionType(i);
            }
        }

        if (Keyboard.current.hKey.wasPressedThisFrame)
            hintRoot.SetActive(!hintRoot.activeSelf);

    }

    public void ToggleHints()
    {
        hintRoot.SetActive(!hintRoot.activeSelf);
    }

    public void SetVisionType(int index)
    {
        foreach (var go in visionTypes)
            go.SetActive(false);

        visionTypes[index].SetActive(true);

        currentVisionType = index;

        //TODO mark which type each is instead of hardcode
        forwardInstRoot.SetActive(index <= 2);
        sideInstRoot.SetActive(index > 2 && index < 6);
        freeInstRoot.SetActive(index == 6);
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
