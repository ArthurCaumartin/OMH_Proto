using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleSyringe : Interactibles
{
    [SerializeField] private GameEvent _getSyringe;

    public override void Interact()
    {
        if (!_isPlayerInRange) return;

        FloatVariable syringeValue = new FloatVariable();

        for (int i = 0; i < _infosManager._variables.Count; i++)
        {
            if (_infosManager._variables[i]._variableName == "Syringe")
            {
                syringeValue = _infosManager._variables[i]._floatVariable;
                break;
            }
        }

        syringeValue.Value += 1f;

        _getSyringe.Raise();

        Destroy(gameObject);
    }
}
