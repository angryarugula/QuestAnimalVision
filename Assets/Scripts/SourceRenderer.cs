using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SourceRenderer : MonoBehaviour
{
    public enum Side { Left, Right };

    public Side side;

    public Vector4 channelScalar = Vector4.one;
    public float saturation = 1;
    [Min(0)]
    public float pixelation = 0;
    [Min(0)]
    public int mipLevel;

    [Min(0)]
    public float vibrance = 1;
    Renderer rend;

    public bool updateProperties;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
    }


    private void OnEnable()
    {
        StartCoroutine(Init());
    }

    IEnumerator Init()
    {
        while (!SourceManager.Ready)
            yield return null;

        UpdateProps();


    }

    void UpdateProps()
    {
        MaterialPropertyBlock block = new MaterialPropertyBlock();
        rend.GetPropertyBlock(block);
        block.SetTexture("main", side == Side.Left ? SourceManager.Left : SourceManager.Right);
        block.SetVector("scalar", channelScalar);
        block.SetFloat("sat", saturation);
        block.SetFloat("pixel", pixelation);
        block.SetFloat("mipLevel", mipLevel);
        block.SetFloat("vib", vibrance);
        rend.SetPropertyBlock(block);
        
        
    }

    private void Update()
    {
        if (updateProperties)
        {
            UpdateProps();
        }
    }
}
