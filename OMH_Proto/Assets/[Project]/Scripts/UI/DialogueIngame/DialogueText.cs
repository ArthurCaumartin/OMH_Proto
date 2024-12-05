using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _dialogueText;
    [SerializeField] private FloatVariable _textduration;

    public void SetDialogueText(string text)
    {
        _dialogueText.text = text;
        StartCoroutine(TextTimer());
    }

    private IEnumerator TextTimer()
    {
        yield return new WaitForSeconds(_textduration.Value);
        _dialogueText.text = "";
    }
}
