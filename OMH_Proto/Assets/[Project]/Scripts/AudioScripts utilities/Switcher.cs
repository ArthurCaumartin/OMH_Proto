using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switcher : MonoBehaviour
{
    [SerializeField] private AK.Wwise.Switch _isActive;

    public void OnEnable()
    {
        _isActive.SetValue(this.gameObject);
    }
}
