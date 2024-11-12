using System;
using UnityEngine;

//! https://www.youtube.com/watch?v=raQ3iHhE_Kk&t=1s

public enum PropertyType { Constant, Variable }

[Serializable]
public class FloatReference
{
    public PropertyType propertyType = PropertyType.Constant;
    [SerializeField] private float constantValue;
    [SerializeField] private FloatVariable variable;

    public float Value
    {
        get
        {
            if (propertyType == PropertyType.Constant)
                return constantValue;
            else
                return variable.Value;
        }

        set
        {
            if (propertyType == PropertyType.Constant)
                constantValue = value;
            else
                variable.Value = value;
        }
    }
}
