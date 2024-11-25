using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private GameEvent _onPlayerInteract, _onOpenPanel;
    
    public void OnInteract()
    {
        print("Interact");
        _onPlayerInteract.Raise();
    }
    
    public void OnPannel()
    {
        _onOpenPanel.Raise();
    }
}
