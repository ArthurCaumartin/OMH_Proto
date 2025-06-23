using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTEsounds : MonoBehaviour
{

    [SerializeField] private AK.Wwise.Event _QTE_Right;
    [SerializeField] private AK.Wwise.Event _QTE_Wrong;
    [SerializeField] private AK.Wwise.Event _QTE_Input;


    public void QTE_Right()
    {
        _QTE_Right.Post(gameObject);
    }

    public void QTE_Wrong()
    {
        _QTE_Wrong.Post(gameObject);
    }
    
    public void QTE_Input()
    {
        _QTE_Input.Post(gameObject);
    }
}
