using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _centerRoom;
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
            room.GetComponent<MapMouseOver>().TracePixelRoom(transform, _radius);
            Destroy(this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print(_centerRoom);
            room.GetComponent<MapMouseOver>().TracePixelRoom(_centerRoom.transform, _radius);
        }
    }

    public void OnDrawGizmos()
    {
        // if(!DEBUG) return;
        Gizmos.color = new Color(0, 0, 1, .2f);
        Gizmos.DrawSphere(transform.position, 3.25f);
    }
}
