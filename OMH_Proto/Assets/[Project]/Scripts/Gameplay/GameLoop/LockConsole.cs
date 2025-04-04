using UnityEngine;
using UnityEngine.UI;

public class LockConsole : Interactible
{
    [SerializeField] private GameObject _doorGameobject, _mapPinLock;
    [SerializeField] private FloatReference _keyInfos;
    [SerializeField] private GameEvent _updateKey, _navMeshUpdate, _cantLock;
    
    [SerializeField] private Sprite _closedLockSprite;
    [SerializeField] private Animator _lockAnimator;

    public void Awake()
    {
        _doorGameobject.SetActive(false);
    }
    
    public override void Interact(PlayerInteract playerInteract, out bool canelInteraction)
    {
        canelInteraction = false;

        if (_keyInfos.Value <= 0)
        {
            _cantLock.Raise();
            return;
        }

        _lockAnimator.SetTrigger("LockClosed");
        
        _doorGameobject.SetActive(true);
        _mapPinLock.SetActive(true);
        
        _navMeshUpdate.Raise();
        
        _keyInfos.Value --;

        _mapPinLock.GetComponent<SpriteRenderer>().sprite = _closedLockSprite;
        
        Destroy(gameObject); 
    }
}
