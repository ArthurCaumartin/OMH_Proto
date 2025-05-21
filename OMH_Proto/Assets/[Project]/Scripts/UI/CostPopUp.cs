using TMPro;
using UnityEngine;

public class CostPopUp : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = .3f;
    [SerializeField] private float _alphaSpeed = .3f;
    private TextMeshProUGUI _text;
    private void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        transform.Translate(Vector2.down * _moveSpeed * Time.deltaTime);
        _text.color = new Color(_text.color.r, _text.color.g, _text.color.b, _text.color.a - (Time.deltaTime * _alphaSpeed));
        if (_text.color.a <= 0) Destroy(gameObject);
    }
}