using System.Collections;
using System.Collections.Generic;
// using UnityEditor.Rendering.Universal.ShaderGUI;
using UnityEngine;

[CreateAssetMenu(fileName = "Profile", menuName = "BalanceProfile")]
public class BalanceProfile : ScriptableObject
{
    [SerializeField] private BalanceProfile _templateToCopy;
    public List<SetterContainer> containerList = new List<SetterContainer>();

    private void Reset()
    {
        containerList.Add(new SetterContainer("Global"));
        containerList.Add(new SetterContainer("Player"));
        containerList.Add(new SetterContainer("Defense"));
        containerList.Add(new SetterContainer("Mobs"));
        containerList.Add(new SetterContainer("Time"));
    }

    private void OnValidate()
    {
        SetConstantName();
    }

    private void SetConstantName()
    {
        for (int i = 0; i < containerList.Count; i++)
        {
            for (int j = 0; j < containerList[i].setterList.Count; j++)
            {
                if (containerList[i].setterList[j].variable)
                    containerList[i].setterList[j].name = containerList[i].setterList[j].variable.name;
            }
        }
    }

    public void BakeValues()
    {
        for (int i = 0; i < containerList.Count; i++)
        {
            for (int j = 0; j < containerList[i].setterList.Count; j++)
            {
                if (containerList[i].setterList[j].variable)
                    containerList[i].setterList[j].variable.Value = containerList[i].setterList[j].constant;
            }
        }
    }

    public void CopyTemplate()
    {
        if (!_templateToCopy) return;
        containerList = _templateToCopy.containerList;
        _templateToCopy = null;
    }
}


