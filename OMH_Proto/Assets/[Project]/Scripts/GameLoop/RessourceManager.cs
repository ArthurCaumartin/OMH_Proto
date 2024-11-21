using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RessourceManager : MonoBehaviour
{
    [SerializeField] private FloatReference _metalScriptable;

    public void GainRessource()
    {
        _metalScriptable.Value ++;
    }
}
