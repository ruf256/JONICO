using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class VolumeWeightSmooth : MonoBehaviour
{
    private Volume vol;

    private void Awake()
    {
        vol = GetComponent<Volume>();
    }

    void Update()
    {
        if(gameObject.activeSelf && vol.weight < 1)
        {
            vol.weight = Mathf.Lerp(vol.weight, 1, Time.deltaTime);
        }
    }
}
