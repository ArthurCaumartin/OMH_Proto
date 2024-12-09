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
    [SerializeField] private GameObject _dialogueBox;
    [SerializeField] private FloatReference _printCharacterDelay, _printDelayAfterWritten;
    private bool _isPrinting = false;
    
    private List<string> _dialogueLines = new List<string>();

    public void PrintNewDialogue(string dialogue)
    {
        if (!gameObject.activeSelf) return;
        // print("Dialogue Box set text");
        //! coroutine send warning if call while enable is false
        if (dialogue == "") return;
        
        foreach (var line in _dialogueLines)
        {
            if (dialogue == line)
            {
                return;
            }
        }
        _dialogueLines.Add(dialogue);
        
        if (!_isPrinting)
            StartCoroutine(PrintText(dialogue));
    }

    private IEnumerator PrintText(string toPrint)
    {
        _isPrinting = true;
        _textMesh.text = "";
        _dialogueBox.SetActive(true);
        
        for (int i = 0; i < toPrint.Length; i++)
        {
            _textMesh.text += toPrint[i];
            yield return new WaitForSeconds(_printCharacterDelay.Value);
        }
        
        yield return new WaitForSeconds(_printDelayAfterWritten.Value);
        _dialogueLines.RemoveAt(0);
        
        _isPrinting = false;
        _dialogueBox.SetActive(false);
        
        if(_dialogueLines.Count > 0) StartCoroutine(PrintText(_dialogueLines[0]));
        
    }
}
