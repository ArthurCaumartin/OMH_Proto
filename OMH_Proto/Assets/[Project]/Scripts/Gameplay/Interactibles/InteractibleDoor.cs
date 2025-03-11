using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleDoor : Interactible
{
    private DoorRoom _doorRoom;

    public override void OnQTEWin()
    {
        _doorRoom.UnlockDoor();
        
        gameObject.layer = LayerMask.NameToLayer("Default");
    }

    public override void OnQTEKill()
    {
        _doorRoom.TempSealDoor();
        
        Destroy(gameObject);
    }

    public void InitializeDoor(DoorRoom door)
    {
        _doorRoom = door;
        
        _doorRoom.LockDoor();
    }
}
