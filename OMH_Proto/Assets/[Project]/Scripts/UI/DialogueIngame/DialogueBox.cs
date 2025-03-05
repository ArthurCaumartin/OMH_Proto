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
    private float _printDelayTimer;
    private int _characterIndex;
    private string _dialogueText;
    
    private List<string> _dialogueLines = new List<string>();

    public void PrintNewDialogue(string dialogue)
    {
        if (!gameObject.activeSelf) return;
        if (dialogue == "") return;
        
        foreach (var line in _dialogueLines)
        {
            if (dialogue == line)
            {
                return;
            }
        }
        _dialogueLines.Add(dialogue);

        if (!_isPrinting) PrintText(dialogue);
    }

    private void PrintText(string toPrint)
    {
        _isPrinting = true;
        _textMesh.text = "";
        _dialogueBox.SetActive(true);
        
        for (int i = 0; i < toPrint.Length; i++)
        {
            _textMesh.text += toPrint[i];
        }
    }

    private void FinishPrintText()
    {
        _dialogueLines.RemoveAt(0);
        
        _isPrinting = false;
        _dialogueBox.SetActive(false);
        _printDelayTimer = 0;
        _characterIndex = 0;
        
        if(_dialogueLines.Count > 0) PrintText(_dialogueLines[0]);
    }

    private void Update()
    {
        if (_isPrinting)
        {
            _printDelayTimer += Time.deltaTime;
            
            if(_printDelayTimer >= _printDelayAfterWritten.Value) FinishPrintText();
        }
    }
}
