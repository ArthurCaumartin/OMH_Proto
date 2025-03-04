using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class QTECode : Upgradable
{
    private QTE _qte;
    private QTECodeUI _qteUi;
    
    private int[] _secretCode;
    private int _codeSelectIndex = 0;
    
    private void Start()
    {
        _secretCode = new int[6];
        for (int i = 0; i < _secretCode.Length; i++)
        {
            _secretCode[i] = Random.Range(0, 10);
        }
        _qteUi = GetComponent<QTECodeUI>();
    }

    public void StartCode(QTE qteManager)
    {
        
        _qte = qteManager;
    }

    public void ResetCode()
    {
        _codeSelectIndex = 0;
    }

    public void SelectCode(int code)
    {
        if (code == _secretCode[_codeSelectIndex])
        {
            _codeSelectIndex ++;
            _qteUi.ActivateUI();
        }
    }

    public void WrongCode()
    {
        //FeedBack 
        _codeSelectIndex = 0;
    }
}
