using System;

//! https://www.youtube.com/watch?v=raQ3iHhE_Kk&t=1s

public enum PropertyType { Constant, Variable }

[Serializable]
public class FloatReference
{
    public PropertyType propertyType = PropertyType.Constant;
    public float constantValue;
    public FloatVariable variable;

    public float Value
    {
        get
        {
            if (propertyType == PropertyType.Constant)
                return constantValue;
            else
                return constantValue;
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
