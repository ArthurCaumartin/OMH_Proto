using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpPopup : MonoBehaviour
{
    [SerializeField] private GameObject _thanatosPopup, _syntaliumPopup, _lockPopup, _barrierPopup, _infoPopup, _infoPopup2;

    [SerializeField] private float _timerToDepop = 5f;
    
    private float _timer;
    private bool _isThanatosShowed, _isSyntaliumShowed, _isLockShowed, _isSomethingShowed, _isBarrierShowed, _isInfoShowed, _isInfoShowed2;
    
    private void Start()
    {
        Depopup();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1)) BarrierPopup();
        
        if (!_isSomethingShowed) return;
        
        // _timer += Time.deltaTime;
        if (_timer >= _timerToDepop)
        {
            _timer = 0;
            Depopup();
        }
    }

    private void Depopup()
    {
        _thanatosPopup.SetActive(false);
        _syntaliumPopup.SetActive(false);
        _lockPopup.SetActive(false);
        _barrierPopup.SetActive(false);
        _infoPopup.SetActive(false);
        _infoPopup2.SetActive(false);

        _isSomethingShowed = false;
    }

    public void ThanatosPopup()
    {
        if (_isThanatosShowed) return;
        _isThanatosShowed = true;
        _isSomethingShowed = true;
        
        _timer = 0;
        _syntaliumPopup.SetActive(false);
        _lockPopup.SetActive(false);
        _thanatosPopup.SetActive(true);
    }
    
    public void SyntaliumPopup()
    {
        if (_isSyntaliumShowed) return;
        _isSyntaliumShowed = true;
        _isSomethingShowed = true;
        
        _timer = 0;
        _lockPopup.SetActive(false);
        _thanatosPopup.SetActive(false);
        _syntaliumPopup.SetActive(true);
    }
    
    public void LockPopup()
    {
        if (_isLockShowed) return;
        _isLockShowed = true;
        _isSomethingShowed = true;
        
        _timer = 0;
        _thanatosPopup.SetActive(false);
        _syntaliumPopup.SetActive(false);
        _lockPopup.SetActive(true);
    }
    
    public void BarrierPopup()
    {
        if (_isBarrierShowed) return;
        _isBarrierShowed = true;
        _isSomethingShowed = true;
        
        _timer = 0;
        _thanatosPopup.SetActive(false);
        _syntaliumPopup.SetActive(false);
        _lockPopup.SetActive(false);
        _barrierPopup.SetActive(true);
    }
    
    public void InfoPopup()
    {
        if (_isInfoShowed) return;
        _isInfoShowed = true;
        _isSomethingShowed = true;
        
        _timer = 0;
        _thanatosPopup.SetActive(false);
        _syntaliumPopup.SetActive(false);
        _lockPopup.SetActive(false);
        _barrierPopup.SetActive(false);
        _infoPopup.SetActive(true);
    }
    
    public void InfoPopup2()
    {
        if (_isInfoShowed2) return;
        _isInfoShowed2 = true;
        _isSomethingShowed = true;
        
        _timer = 0;
        _thanatosPopup.SetActive(false);
        _syntaliumPopup.SetActive(false);
        _lockPopup.SetActive(false);
        _barrierPopup.SetActive(false);
        _infoPopup2.SetActive(true);
    }
}
