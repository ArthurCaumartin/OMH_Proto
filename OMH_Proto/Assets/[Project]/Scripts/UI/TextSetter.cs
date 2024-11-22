using TMPro;
using UnityEngine;

public class TextSetter : MonoBehaviour
{
    [TextArea] public string textToSet;
    [SerializeField] private TextMeshProUGUI _text;

    public void SetText()
    {
        _text.text  = textToSet;
    }
}
