using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RoomTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _centerRoom;
    [SerializeField] private List<GameObject> _roomHiders;
    [SerializeField] private int _radius;
    [SerializeField] private bool _isObjRoom;

    private bool _isDiscovered, _isFadeFinished;
    private GameObject room;
    private List<Material> _roomHiderMat = new List<Material>();
    private float _roomHidingFloat;
    
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

    // private void OnTriggerEnter(Collider other)
    // {
    //     if(_isDiscovered) return;
    //     
    //     if (other.CompareTag("Player"))
    //     {
    //         _isDiscovered = true;
    //         EntryRoom();
    //     }
    // }

    public void EntryRoom()
    {
        if(_isDiscovered) return;
        _isDiscovered = true;
        
        if (_centerRoom != null) room.GetComponent<MapFogOfWar>().TracePixelRoom(_centerRoom.transform, _radius);

        if (_roomHiders.Count != 0)
        {
            _roomHidingFloat = 1;
            DOTween.To(() => _roomHidingFloat, x => _roomHidingFloat = x, 0f, 1f);

            for (int i = 0; i < _roomHiders.Count; i++)
            {
                _roomHiderMat.Add(_roomHiders[i].GetComponent<MeshRenderer>().material);
            }

            StartCoroutine(FadeOutRoomHider());
        }
    }

    private void Update()
    {
        if (_isDiscovered && !_isFadeFinished)
        {
            for (int i = 0; i < _roomHiderMat.Count; i++)
            {
                _roomHiderMat[i].SetFloat("_Alpha", _roomHidingFloat);
            }
        }
    }

    private IEnumerator FadeOutRoomHider()
    {
        yield return new WaitForSeconds(1.1f);
        _isFadeFinished = true;
    }

    // public void OnDrawGizmos()
    // {
    //     // if(!DEBUG) return;
    //     Gizmos.color = new Color(0, 0, 1, .2f);
    //     Gizmos.DrawSphere(transform.position, 3.25f);
    // }
}
