using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextSequence : MonoBehaviour
{
    [Serializable] public struct Sequence { public string name; public List<string> strings; }
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private float _speed = 1;
    [SerializeField] private int _indexToPlay = 0;
    [SerializeField] private List<Sequence> _sequence;
    private float _time;

    public int IndexToPlay { set => _indexToPlay = value; }

    private void Update()
    {
        _time += Time.deltaTime * _speed;
        _text.text = _sequence[_indexToPlay].strings[(int)(_time % _sequence[_indexToPlay].strings.Count)];
    }
}
