using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndGameManager : MonoBehaviour
{
    [SerializeField] private GameChooseMeta _gameChooseMeta;
    [SerializeField] private FloatReference _syphonHealth, _pcen, _scrapMetal;
    [SerializeField] private FloatReference _gameTime, _defenseDuration, explorationDuration;
    [Space] [SerializeField] private PlayerItemList _playerItemList;
    [SerializeField] private ObjectUIManager _objectUIManager;
    [Space] [SerializeField] private TextMeshProUGUI _titleText, _lostHpText;
    [SerializeField] private GameObject _pcenFromTime, _pcenFromMetal, _pcenLost, _pcenText;
    [SerializeField] private Image _weaponImage;

    [Space]
    
    [SerializeField, Tooltip("pcen lost each hp lost")] private float _costEachHealthLost = 2;
    [SerializeField, Tooltip("pcen gain each defend second")] private float _valueEachSecond = 10;
    [SerializeField, Tooltip("NumberMetals / x pcen to gain")] private float _valueDivisionMetal = 10;
    
    void Start()
    {
        // _pcenText.GetComponent<TextMeshProUGUI>().text = _pcen.Value.ToString();
        for (int i = 0; i < _playerItemList._items.Count; i++)
        {
            ItemScriptable tempItem = _playerItemList._items[i];
            _objectUIManager.AddObjectUI(tempItem._itemName, tempItem._itemDescription, tempItem._itemSprite);
        }

        _weaponImage.sprite = _gameChooseMeta._weaponChoose._weaponIcon;
        _lostHpText.text = 20 - _syphonHealth.Value + " HP Lost :";
        
        if (_syphonHealth.Value <= 0)
        {
            LostGame();
        }
        else
        {
            WinGame();
        }
    }

    public void LostGame()
    {
        _titleText.text = "You lost !";
        

        float defenseTimeBeforeLost = _gameTime.Value;
        int pcenGainFromTime = Mathf.RoundToInt(defenseTimeBeforeLost * _valueEachSecond);
        
        SetupValues(pcenGainFromTime);
    }

    public void WinGame()
    {
        _titleText.text = "You won !";

        int pcenGainFromTime = Mathf.RoundToInt(_defenseDuration.Value * _valueEachSecond);
        
        SetupValues(pcenGainFromTime);
    }

    private void SetupValues(int pcenTime)
    {
        StartCoroutine(AddPcen(pcenTime));
    }

    private void ChangeText(GameObject textObject, int value)
    {
        textObject.GetComponent<UpdateTextAnimation>().ChangeTextAnimation(value);
    }

    private IEnumerator AddPcen(int pcenTime)
    {
        int pcenGainFromMetal = Mathf.RoundToInt(_scrapMetal.Value / _valueDivisionMetal);
        int pcenLostFromSyphonHealth = (int)((20 - _syphonHealth.Value) * _costEachHealthLost);
        
        _pcen.Value = pcenTime + pcenGainFromMetal + pcenLostFromSyphonHealth;
        
        yield return new WaitForSeconds(2);
        
        ChangeText(_pcenFromTime, pcenTime);
        yield return new WaitForSeconds(2);
        
        ChangeText(_pcenFromMetal, pcenGainFromMetal);

        if (pcenLostFromSyphonHealth != 0)
        {
            yield return new WaitForSeconds(2);
            ChangeText(_pcenLost, pcenLostFromSyphonHealth);
        }
        
        yield return new WaitForSeconds(2);
        
        ChangeText(_pcenFromTime, 0);
        ChangeText(_pcenText, pcenTime);
        yield return new WaitForSeconds(2);
        
        ChangeText(_pcenFromMetal, 0);
        ChangeText(_pcenText, pcenGainFromMetal + pcenTime);
        yield return new WaitForSeconds(2);
        
        ChangeText(_pcenText, pcenTime + pcenGainFromMetal - pcenLostFromSyphonHealth);
        ChangeText(_pcenLost, 0);
    }
}
