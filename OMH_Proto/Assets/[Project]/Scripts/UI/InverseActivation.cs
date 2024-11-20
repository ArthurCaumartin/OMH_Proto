using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InverseActivation : MonoBehaviour
{
    [SerializeField] private GameObject _objectToModify;
    public void InverseActivationObject()
    {
        _objectToModify.SetActive(!_objectToModify);
    } 
}
