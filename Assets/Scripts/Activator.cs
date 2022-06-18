using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour
{
    public GameObject hint;

    void OnEnable()
    {
        hint.SetActive(true);
    }

    void OnDisable()
    {
        hint.SetActive(false);
    }
}
