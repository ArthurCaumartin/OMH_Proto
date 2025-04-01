using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleDoor : Interactible
{
    private DoorRoom _doorRoom;
    private RoomManager _roomManager;

    public override void OnQTEWin()
    {
        _doorRoom.UnlockDoor();
        
        gameObject.layer = LayerMask.NameToLayer("Default");
    }

    public override void OnQTEKill()
    {
        _doorRoom.TempSealDoor();

        _roomManager.DoorIsUnlocked();
        Destroy(gameObject);
    }

    public void InitializeDoor(DoorRoom door, RoomManager roomManager)
    {
        _doorRoom = door;
        _roomManager = roomManager;
        
        _doorRoom.LockDoor();
    }

    public void StartDefense()
    {
        _doorRoom.UnlockDoor();
        enabled = false;
        // Destroy(gameObject);
    }
}
