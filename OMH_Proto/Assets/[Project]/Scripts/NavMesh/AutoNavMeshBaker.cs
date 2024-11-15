using Unity.AI.Navigation;
using UnityEngine;

public class AutoNavMeshBaker : MonoBehaviour
{
    private void Awake()
    {
        NavMeshSurface n = GetComponent<NavMeshSurface>();
        if(!n.navMeshData) n.BuildNavMesh();
    }
}
