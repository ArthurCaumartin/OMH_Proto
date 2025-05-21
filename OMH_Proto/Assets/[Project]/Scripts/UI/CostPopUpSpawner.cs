using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Video;

public class CostPopUpSpawner : MonoBehaviour
{
    [SerializeField] private FloatVariable _metal;
    [SerializeField] private TextMeshProUGUI _popUpTextPrefab;
    [Space]
    [SerializeField] private Color _positifColor;
    [SerializeField] private Color _negatifColor;
    private TextMeshProUGUI _text;

    private void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _metal.OnAddValue.AddListener(SpawnPopUp);
    }

    void Update()
    {
        _text.text = _metal.Value.ToString().Split('.')[0];
    }

    private void SpawnPopUp(float quantity)
    {
        print("Pop up : " + quantity);
        TextMeshProUGUI tmp = Instantiate(_popUpTextPrefab, transform.parent);
        tmp.text = (quantity > 0 ? "+" : "") + quantity.ToString();
        tmp.color = quantity > 0 ? _positifColor : _negatifColor;
    }
}
