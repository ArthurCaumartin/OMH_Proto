using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _doors = new List<GameObject>();
    [SerializeField] private GameObject _doorLockInteractiblePrefab;

    private InteractibleDoor _doorLocked;
    private bool _isDoorUnlocked;
    void Awake()
    {
        int _lockedDoorIndex = Random.Range(0, _doors.Count);
        GameObject tempObject = Instantiate(_doorLockInteractiblePrefab, _doors[_lockedDoorIndex].transform);
        
        InteractibleDoor tempInteractible = tempObject.GetComponent<InteractibleDoor>();
        
        tempInteractible.InitializeDoor(_doors[_lockedDoorIndex].GetComponent<DoorRoom>(), this);
        _doorLocked = tempInteractible;
    }

    public void StartDefense()
    {
        if (_isDoorUnlocked) return;
        _doorLocked.StartDefense();
    }

    public void DoorIsUnlocked()
    {
        _isDoorUnlocked = true;
    }
}
