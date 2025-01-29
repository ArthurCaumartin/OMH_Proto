using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public class GridLayout : MonoBehaviour
// {
//     // Start is called before the first frame update

//     public GameObject _mainCamera; 
//     private Vector
//     void Start()
//     {
        
//     }

//     // Update is called once per frame
//     private Vector3 MouseAimPosition(Vector3 currentPos)
//     {
//         Vector2 pixelPos = Input.mousePosition;
//         Ray camRay = _mainCamera.ScreenPointToRay(pixelPos);


//         Physics.Raycast(camRay, out RaycastHit hit, Mathf.Infinity, _aimLayer);
//         if (!hit.collider) return currentPos;

//         if (DEBUG) Debug.DrawRay(camRay.origin, camRay.direction * 100, Color.green);
//         if (DEBUG) Debug.DrawLine(new Vector3(hit.point.x, hit.point.y - 1, hit.point.z)
//                                 , new Vector3(hit.point.x, hit.point.y + 1, hit.point.z)
//                                 , Color.red);

//         return Vector3.Distance(_playerTransform.position, hit.point) > _range.Value ?
//         _playerTransform.position + (hit.point - _playerTransform.position).normalized * _range.Value : hit.point;
//     }
//     void Update()
//     {
        
//     }
// }
