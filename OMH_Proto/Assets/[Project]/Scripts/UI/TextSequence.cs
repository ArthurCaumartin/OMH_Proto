using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextSequence : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private float _speed = 1;
    [SerializeField] private List<string> _strings;
    private float _time;

    private void Update()
    {
        _time += Time.deltaTime * _speed;
        _text.text = _strings[(int)(_time % _strings.Count)];
    }
}
