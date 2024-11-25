using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleKey : Interactibles
{
    [SerializeField] private GameEvent _getKey;

    public override void Interact()
    {
        if (!_isPlayerInRange) return;

        FloatVariable keyValue = new FloatVariable();

        for (int i = 0; i < _infosManager._variables.Count; i++)
        {
            if (_infosManager._variables[i]._variableName == "Key")
            {
                keyValue = _infosManager._variables[i]._floatVariable;
                break;
            }
        }

        keyValue.Value += 1f;

        _getKey.Raise();

        Destroy(gameObject);
    }
}
