using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsPannel : MonoBehaviour
{
    [SerializeField] private List<ChangeText> _statsTexts = new List<ChangeText>();

    public void Start()
    {
        for (int i = 0; i < _statsTexts.Count; i++)
        {
            _statsTexts[i].Initialize();
        }
    }

    public void ChangeStatsTexts()
    {
        for (int i = 0; i < _statsTexts.Count; i++)
        {
            _statsTexts[i].ChangeTextInCanvas();
        }
    }
}
