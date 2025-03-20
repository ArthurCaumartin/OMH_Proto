using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _centerRoom, _lightParent;
    [SerializeField] private int _radius;
    [SerializeField] private bool _isObjRoom;

    private GameObject room;
    private void Awake()
    {
        room = GameObject.FindGameObjectWithTag("MapTexture");
    }

    private void Start()
    {
        if (_isObjRoom)
        {
            if(room != null) room.GetComponent<MapFogOfWar>().TracePixelRoom(transform, _radius);
            Destroy(this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            if (_centerRoom != null) room.GetComponent<MapFogOfWar>().TracePixelRoom(_centerRoom.transform, _radius);
            if(_lightParent != null) _lightParent.SetActive(true);
        }
    }

    public void OnDrawGizmos()
    {
        // if(!DEBUG) return;
        Gizmos.color = new Color(0, 0, 1, .2f);
        Gizmos.DrawSphere(transform.position, 3.25f);
    }
}
