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
    private bool _isWrondCode;
    
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
        _qteUi.ActivateUI();
        _qte = qteManager;
    }

    public void ResetCode()
    {
        _codeSelectIndex = 0;
        _qteUi.ResetText();
    }

    public void SelectCode(int code)
    {
        if(_isWrondCode) return;
        if(_codeSelectIndex >= _secretCode.Length) return;
        
        if (code == _secretCode[_codeSelectIndex])
        {
            _codeSelectIndex ++;
            _qteUi.SetGoodInputFeedBack(code);
            
            if (_codeSelectIndex >= _secretCode.Length - 1) WinCode();
        }
        else
        {
            _codeSelectIndex = 0;
            _qteUi.SetBadInputFeedBack();
            StartCoroutine(WrongNumber());
        }
    }

    private void WinCode()
    {
        _qteUi.WinCode();
        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
        _qte.KillQTE();
    }
    
    private IEnumerator WrongNumber()
    {
        _isWrondCode = true;
        yield return new WaitForSeconds(0.7f);
        _isWrondCode = false;
    }
}
