using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleKey : Interactible
{
    [Space]
    [SerializeField] private GameEvent _getKey;
    [SerializeField] private FloatVariable keyValue;
    [SerializeField] private MeshRenderer _mesh;
    
    private bool _isKeyGet = false;
    
    public override void Interact(PlayerInteract playerInteract, out bool cancelInteraction)
    {
        cancelInteraction = true;
        if (_isKeyGet) return;
        _isKeyGet = true;
        
        keyValue.Value += 1f;
        _getKey.Raise();
        
        _mesh.transform.parent = null;
        Destroy(gameObject);
    }
}
