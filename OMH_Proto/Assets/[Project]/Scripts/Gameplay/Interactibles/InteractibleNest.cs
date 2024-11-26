using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleNest : Interactible
{
    [Space]
    [SerializeField] private FloatVariable _syringeValue;
    [SerializeField] private GameEvent _destroyNest;
    [SerializeField] private GameObject _enemySpawner;

    public override void Interact(out bool cancelInteraction)
    {
        cancelInteraction = _syringeValue.Value <= 0;
    }

    public override void OnQTEWin()
    {
        _syringeValue.Value -= 1;
        _destroyNest.Raise();

        if (_enemySpawner) _enemySpawner.BroadcastMessage("DestroyNest");

        Destroy(gameObject);
    }
}
