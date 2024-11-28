using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    // [SerializeField] private MapPart _mapPartOfRoom;
    [SerializeField] private GameObject _mapPart1, _mapPart2;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _mapPart1.SetActive(true);
            _mapPart2.SetActive(true);
            // _mapPartOfRoom.PlayerInRoom();
        }
    }
    
    // private void OnTriggerExit(Collider other)
    // {
    //     if (other.CompareTag("Player"))
    //     {
    //          _mapPartOfRoom.PlayerNotInRoom();
    //     }
    // }

    public void OnDrawGizmos()
    {
        // if(!DEBUG) return;
        Gizmos.color = new Color(0, 0, 1, .2f);
        Gizmos.DrawSphere(transform.position, 3.25f);
    }
}
