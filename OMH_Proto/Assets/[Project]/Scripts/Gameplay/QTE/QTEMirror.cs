using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class QTEMirror : Upgradable
{
    [SerializeField] private int _numbersOfCode;
    
    private QTE _qte;
    private QTEMirrorUI _qteUi;
    
    private int _winIndex = 0;
    private Dictionary<int, bool> _valuesDictionnary = new Dictionary<int, bool>();
    
    private void Start()
    {
        _qteUi = GetComponent<QTEMirrorUI>();
    }

    private int[] GetRandomArray(int lenght)
    {
        bool isGood = true;
        
        int[] array = new int[lenght];

        while (isGood)
        {
            int isArrayGood = 0;
            
            for (int i = 0; i < lenght; i++)
            {
                array[i] = Random.Range(0, 6);
            }

            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array.Length; j++)
                {
                    if (j != i)
                    {
                        if (array[i] == array[j])
                        {
                            isArrayGood++;
                        }
                    }
                }
            }

            if (isArrayGood == 0)
            {
                isGood = false;
                return array;
            }
        }
        return null;
    }

    public void StartQTE(QTE qteManager)
    {
        _qte = qteManager;
        _qteUi.InitializeUI(_numbersOfCode);
        NewCode();
    }

    private void NewCode()
    {
        _valuesDictionnary = new Dictionary<int, bool>();
        int[] tempArray = GetRandomArray(3);
        
        _qteUi.NewQTE(tempArray, _winIndex, _numbersOfCode);
        for (int i = 0; i < tempArray.Length; i++)
        {
            _valuesDictionnary[tempArray[i]] = false;
        }
    }

    public void ResetCode()
    {
        _winIndex = 0;
        _qteUi.ResetQTE();
    }

    public void SelectCode(int code)
    {
        foreach (KeyValuePair<int, bool> kvp in _valuesDictionnary)
        {
            if (kvp.Key == code)
            {
                if (kvp.Value == false)
                {
                    _valuesDictionnary[code] = true;
                    _qteUi.SetGoodInputFeedBack(code);
                    VerifyCodeIsGood();
                    
                    return;
                }
            }
        }
        
        _qteUi.SetBadInputFeedBack();
        foreach (KeyValuePair<int, bool> kvp in _valuesDictionnary.ToList())
        {
            _valuesDictionnary[kvp.Key] = false;
        }
    }

    private void VerifyCodeIsGood()
    {
        foreach (KeyValuePair<int, bool> kvp in _valuesDictionnary)
        {
            if(kvp.Value == false) return;
        }

        _winIndex++;
        if(_winIndex >= _numbersOfCode) WinCode();
        else NewCode();
    }

    private void WinCode()
    {
        _qteUi.WinCode();
        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
        _qte.OnWin.Invoke();
    }
}
