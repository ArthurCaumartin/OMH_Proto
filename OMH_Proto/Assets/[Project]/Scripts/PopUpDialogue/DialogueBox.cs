using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueBox : MonoBehaviour
{
    public static DialogueBox instance;
    private void Awake() { if (instance) Destroy(gameObject); instance = this; }

    [SerializeField] private TextMeshProUGUI _textMesh;
    [SerializeField] private FloatReference _printCharacterDelay;
    private bool _isPrinting = false;

    public void PrintNewDialogue(string dialogue)
    {
        if (!gameObject.activeSelf) return;
        // print("Dialogue Box set text");
        //! coroutine send warning if call while enable is false
        if (!_isPrinting)
            StartCoroutine(PrintText(dialogue));
    }

    private IEnumerator PrintText(string toPrint)
    {
        _isPrinting = true;
        _textMesh.text = "";
        for (int i = 0; i < toPrint.Length; i++)
        {
            _textMesh.text += toPrint[i];
            yield return new WaitForSeconds(_printCharacterDelay.Value);
        }
        _isPrinting = false;
    }
}
