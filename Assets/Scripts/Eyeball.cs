using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Eyeball : MonoBehaviour
{
    static List<string> usedDevices = new List<string>();

    WebCamTexture texture;

    public Vector2Int resolution;
    public string deviceHint;

    public float delay;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(delay);

        var devices = WebCamTexture.devices;
        var device = devices.FirstOrDefault((x) => x.name.ToLower().Contains(deviceHint.ToLower()) && !usedDevices.Contains(x.name));

        Debug.Log("Device List");
        foreach(var d in devices)
        {
            Debug.Log(d.name);
        }

        if(device.name != "")
        {
            Debug.Log("Connecting to: " + device.name);
            texture = new WebCamTexture(device.name, resolution.x, resolution.y, 60);

            GetComponent<Renderer>().material.mainTexture = texture;
            texture.Play();

            usedDevices.Add(device.name);
        }
    }

}
