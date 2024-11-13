using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private GameEvent _onPlayerInteract;
    
    public void OnInteract()
    {
        _onPlayerInteract.Raise();
    }
}
