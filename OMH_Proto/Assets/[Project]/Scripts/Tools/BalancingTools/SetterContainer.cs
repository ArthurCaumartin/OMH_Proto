using System;
using System.Collections.Generic;

[Serializable]
public class SetterContainer
{
    public string name;
    public List<ValueSetter> setterList = new List<ValueSetter>();

    public SetterContainer(string name)
    {
        this.name = name;
    }
}


