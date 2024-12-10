using UnityEngine;

public class InteractibleSyringe : Interactible
{
    [Space]
    [SerializeField] private FloatVariable _syringeValue;
    [SerializeField] private GameEvent _getSyringe;

    public override void Interact(out bool cancelInteraction)
    {
        cancelInteraction = false;
        _syringeValue.Value += 1f;
        _getSyringe.Raise();
        // Destroy(gameObject);
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        boxCollider.enabled = false;
    }
}
