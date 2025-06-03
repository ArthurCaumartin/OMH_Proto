using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerInfoText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private List<GameObject> _timerInfoTexts = new List<GameObject>();

    private float _explorationTimer = 0;
    private bool _isDefenseStarted = false;

    private void Update()
    {
        if (_isDefenseStarted)
        {
            _timerInfoTexts[7].SetActive(true);
            return;
        }

        DeactivateAllText();
        
        _explorationTimer += Time.deltaTime;
        
        //Explore the base
        if(_explorationTimer < 240) _timerInfoTexts[0].SetActive(true);
        
        //Can almost start
        else if(_explorationTimer < 300) _timerInfoTexts[1].SetActive(true);
        
        //Can start, wave 1
        else if(_explorationTimer < 360) _timerInfoTexts[2].SetActive(true);
        
        //Can start, wave 2
        else if(_explorationTimer < 420) _timerInfoTexts[3].SetActive(true);
        
        //Can start, wave 3
        else if(_explorationTimer < 480) _timerInfoTexts[4].SetActive(true);
        
        //Can start, wave 4
        else if(_explorationTimer < 540) _timerInfoTexts[5].SetActive(true);
        
        //Can start, wave 5
        else if(_explorationTimer < 600) _timerInfoTexts[6].SetActive(true);
    }

    private void DeactivateAllText()
    {
        for (int i = 0; i < _timerInfoTexts.Count; i++)
        {
            _timerInfoTexts[i].SetActive(false);
        }
    }

    public void DefenseStart()
    {
        _isDefenseStarted = true;
    }
}
