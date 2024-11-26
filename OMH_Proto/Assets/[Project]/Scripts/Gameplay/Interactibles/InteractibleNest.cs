using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleNest : Interactible
{
    [SerializeField] private GameEvent _destroyNest;

    [SerializeField] private GameObject _enemySpawner;

    // public override void Interact()
    // {
    //     if (!_isPlayerInRange) return;

    //     FloatVariable syringeValue = new FloatVariable();
        
    //     for (int i = 0; i < _infosManager._variables.Count; i++)
    //     {
    //         if (_infosManager._variables[i]._variableName == "Syringe")
    //         {
    //             syringeValue = _infosManager._variables[i]._floatVariable;
    //             break;
    //         }
    //     }
        
    //     if (syringeValue.Value <= 0)
    //     {
    //         Debug.Log("Dont have enough SERINGE");
    //         return;
    //     }

    //     syringeValue.Value -= 1;
    //     _destroyNest.Raise();
        
    //     _enemySpawner.BroadcastMessage("DestroyNest");
        
    //     Destroy(gameObject);
    // }
}
