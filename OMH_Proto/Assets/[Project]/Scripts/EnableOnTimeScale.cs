using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableOnTimeScale : MonoBehaviour
{
    [SerializeField] private Behaviour _obj;

    void Update()
    {
        _obj.enabled = Time.timeScale == 1;
    }
}
