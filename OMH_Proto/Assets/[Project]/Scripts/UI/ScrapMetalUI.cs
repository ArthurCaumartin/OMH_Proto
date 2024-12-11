using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapMetalUI : MonoBehaviour
{
    [SerializeField] private List<GameObject> _metalGeneratorPointList = new List<GameObject>();
    private int _tempFloat = 0;
    
    public void ActivateGenerator()
    {
        if (_metalGeneratorPointList.Count < _tempFloat) return;
        
        _metalGeneratorPointList[_tempFloat].SetActive(true);
        _tempFloat ++; 
    }
}
