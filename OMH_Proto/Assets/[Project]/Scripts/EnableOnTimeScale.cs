using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableOnTimeScale : MonoBehaviour
{
    [SerializeField] private Behaviour _obj;

    void Update()
    {
        if (!_obj)
        {
            enabled = false;
            Debug.LogError("Not Behavior Set on EnableOnTimeScale on : " + name);
            return;
        }
        _obj.enabled = Time.timeScale == 1;
    }
}
