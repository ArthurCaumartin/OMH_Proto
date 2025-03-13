using UnityEngine;
using UnityEngine.UI;

public class LockConsole : Interactible
{
    [SerializeField] private GameObject _doorGameobject, _mapPinLock;
    [SerializeField] private FloatReference _keyInfos;
    [SerializeField] private GameEvent _updateKey, _navMeshUpdate;
    
    [SerializeField] private Sprite _closedLockSprite;

    public void Awake()
    {
        _doorGameobject.SetActive(false);
    }
    
    public override void Interact(PlayerInteract playerInteract, out bool canelInteraction)
    {
        canelInteraction = false;
        
        if (_keyInfos.Value <= 0) return;
        
        _doorGameobject.SetActive(true);
        _mapPinLock.SetActive(true);
        
        _navMeshUpdate.Raise();
        
        _keyInfos.Value --;

        _mapPinLock.GetComponent<MapPin>()._tallMapPin = _closedLockSprite;
        _mapPinLock.GetComponent<SpriteRenderer>().sprite = _closedLockSprite;
        
        Destroy(gameObject); 
    }
}
