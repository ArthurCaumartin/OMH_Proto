using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSpin : MonoBehaviour
{
    public Renderer _r;
    public float _speed;
    [Range(0, 1)] public float s;
    [Range(0, 1)] public float v;

    void Update()
    {
        Color c = Color.HSVToRGB(Mathf.InverseLerp(-1, 1, Mathf.Sin(Time.time * _speed)), s, v);
        _r.material.SetColor("_BaseColor", c);
    }
}
