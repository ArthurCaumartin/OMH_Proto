using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ArmoryCodeManager : MonoBehaviour
{
    [SerializeField] private List<Transform> _codePositions = new List<Transform>();
    [SerializeField] private GameObject _codePrefab;
    [SerializeField] private QTECode _codeScript;
    [SerializeField] private string _code;
    private int randomIndexPosition;
    
    void Start()
    {
        randomIndexPosition = Random.Range(0, _codePositions.Count);
        
        Debug.Assert(_codeScript != null, "ARMORY CODE SCRIPT IS NULL IN ARMORY CODE MANAGER");
        _code = _codeScript.GetCodeManager();
        
        SetCodeInScene(_codePositions[randomIndexPosition]);
    }
    
    void SetCodeInScene(Transform codePosition)
    {
        GameObject _instantiatedObject = Instantiate(_codePrefab, codePosition);
        TextMeshProUGUI textCode = _instantiatedObject.GetComponentInChildren<TextMeshProUGUI>();
        textCode.text = _code;
    }
}
