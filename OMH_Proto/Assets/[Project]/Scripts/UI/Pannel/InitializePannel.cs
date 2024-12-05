using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializePannel : MonoBehaviour
{
    [SerializeField] private GameObject _leftPannel, _rightPannel;
    [SerializeField] private GameEvent _itemEvent;

    void Awake()
    {
        _leftPannel.SetActive(true);
        _rightPannel.SetActive(true);
        _itemEvent.Raise();
    }
    void Start()
    {
        _leftPannel.SetActive(false);
        _rightPannel.SetActive(false);
    }
}
