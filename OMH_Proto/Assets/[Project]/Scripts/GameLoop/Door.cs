using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class Door : MonoBehaviour, IMapClickable
{
    [SerializeField] private GameObject _doorGameobject;
    [SerializeField] private NavMeshSurface _navMesh;

    private bool _isOpen;
    
    public void OnClick()
    {
        if (_isOpen)
        {
            _doorGameobject.SetActive(false);
            _isOpen = false;
            
            _navMesh.BuildNavMesh();
            // Bake le Navmesh
        }
        else
        {
            _doorGameobject.SetActive(true);
            _isOpen = true;
            
            _navMesh.BuildNavMesh();
            // Bake le Navmesh
        }
        
    }
}
