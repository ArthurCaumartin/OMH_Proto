using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedLineClock : MonoBehaviour
{
    [SerializeField] private FloatReference _exploDuration, _defenseDuration;
    [SerializeField] private RectTransform rectTransform;
    
    void Start()
    {
        float tempDefenseDuration = _defenseDuration.Value / 60;
        float tempExploDuration = _exploDuration.Value / 60;
        
        rectTransform.GetComponent<RectTransform>();
        rectTransform.Rotate(new Vector3(0, 0, -(360 / (tempDefenseDuration + tempExploDuration) * tempExploDuration)));
    }
}
