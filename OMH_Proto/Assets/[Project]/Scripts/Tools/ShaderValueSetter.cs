using UnityEngine;
using System;
using System.Collections.Generic;


[CreateAssetMenu(menuName = "ShaderValueSetter")]
public class ShaderValueSetter : ScriptableObject
{
    [Serializable]
    public struct ValueSetter
    {
        public string parametreName;
        public FloatReference valueReference;
    }
    public List<Material> _materialList;
    public List<ValueSetter> _valueSetterList;

    public void SetValue()
    {
        for (int i = 0; i < _materialList.Count; i++)
        {
            for (int j = 0; j < _valueSetterList.Count; j++)
            {
                try
                {
                    _materialList[i].SetFloat(_valueSetterList[j].parametreName, _valueSetterList[j].valueReference.Value);
                }
                catch { }
            }
        }
    }
}
