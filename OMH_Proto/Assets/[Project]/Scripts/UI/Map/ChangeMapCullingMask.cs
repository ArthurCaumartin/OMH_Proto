using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMapCullingMask : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    
    public LayerMask _tallLayerMask, _littleLayerMask;
    
    public void ChangeTallMap()
    {
        _camera.cullingMask = _tallLayerMask;
    }
    
    public void ChangeLittleMap()
    {
        _camera.cullingMask = _littleLayerMask;
    }
}
