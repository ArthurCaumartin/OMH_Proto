using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrapMetalUI : MonoBehaviour
{
    [SerializeField] private List<GameObject> _metalGeneratorPointList = new List<GameObject>();
    [SerializeField] private GameObject _scrapMetalImage;
    [SerializeField] private float _speedPerGenerator;
    private int _tempFloat = 0;
    
    public void ActivateGenerator()
    {
        if (_metalGeneratorPointList.Count < _tempFloat) return;
        
        _metalGeneratorPointList[_tempFloat].SetActive(true);
        _tempFloat ++; 
    }
    void Update()
    {
        _scrapMetalImage.transform.Rotate(-Vector3.forward * Time.deltaTime * (_speedPerGenerator * _tempFloat));
    }
}
