using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "InfosManager", menuName = "Infos")]
public class InfosManager : ScriptableObject
{
    public List<VariablesInfos> _variables = new List<VariablesInfos>();
}

[Serializable]
public class VariablesInfos
{
    public string _variableName;
    public FloatVariable _floatVariable;
    public float _resetValue;

    public void Reset()
    {
        _floatVariable.Value = _resetValue;
    }
}
