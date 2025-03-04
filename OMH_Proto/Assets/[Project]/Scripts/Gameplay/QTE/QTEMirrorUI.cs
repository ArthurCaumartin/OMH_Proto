using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QTEMirrorUI : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Image _textContainerImage;
    [SerializeField] private GameObject _reproduceButtonsParent, _endText;
    
    [SerializeField] private List<GameObject> _reproduceObjects = new List<GameObject>();
    [SerializeField] private List<GameObject> _activateObjects = new List<GameObject>();
    public void StartQTE(int[] intArray)
    {
        for (int i = 0; i < intArray.Length; i++)
        {
            Image tempImage = _reproduceObjects[intArray[i]].GetComponent<Image>();
            tempImage.color = Color.green;
        }
    }

    public void ResetQTE()
    {
        for (int i = 0; i < _activateObjects.Count; i++)
        {
            _activateObjects[i].GetComponent<Image>().color = Color.red;
        }
    }
    
    public void SetGoodInputFeedBack(int value)
    {
        _activateObjects[value].GetComponent<Image>().color = Color.green;
    }

    public void SetBadInputFeedBack()
    {
        for (int i = 0; i < _activateObjects.Count; i++)
        {
            _activateObjects[i].GetComponent<Image>().color = Color.red;
        }
    }

    public void WinCode()
    {
        _reproduceButtonsParent.SetActive(false);
        _endText.SetActive(true);
    }
}
