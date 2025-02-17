using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndGameManager : MonoBehaviour
{
    [SerializeField] private FloatReference _syphonHealth, _pcen, _scrapMetal;
    [SerializeField] private FloatReference _gameTime, _defenseDuration, explorationDuration;
    [Space] [SerializeField] private PlayerItemList _playerItemList;
    [Space] [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private GameObject _pcenFromTime, _pcenFromMetal, _pcenLost, _pcenText;

    [Space]
    
    [SerializeField, Tooltip("pcen lost each hp lost")] private float _costEachHealthLost = 2;
    [SerializeField, Tooltip("pcen gain each defend second")] private float _valueEachSecond = 10;
    [SerializeField, Tooltip("NumberMetals / x pcen to gain")] private float _valueDivisionMetal = 10;


    void Start()
    {
        _pcenText.GetComponent<TextMeshProUGUI>().text = _pcen.Value.ToString();
        
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

        float defenseTimeBeforeLost = _defenseDuration.Value - (_gameTime.Value - explorationDuration.Value);
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
        int pcenGainFromMetal = Mathf.RoundToInt(_scrapMetal.Value / _valueDivisionMetal);
        int pcenLostFromSyphonHealth = (int)((20 - _syphonHealth.Value) * _costEachHealthLost);
        ChangeText(_pcenFromTime, pcenTime);
        ChangeText(_pcenFromMetal, pcenGainFromMetal);
        ChangeText(_pcenLost, pcenLostFromSyphonHealth);
        StartCoroutine(AddPcen(pcenTime, pcenGainFromMetal, pcenLostFromSyphonHealth));
    }

    private void ChangeText(GameObject textObject, int value)
    {
        textObject.GetComponent<UpdateTextAnimation>().ChangeTextAnimation(value);
    }

    private IEnumerator AddPcen(int pcenTime, int pcenMetal, int pcenLost)
    {
        int pcenGainFromTime = pcenTime + pcenMetal + pcenLost;
        yield return new WaitForSeconds(2);
        ChangeText(_pcenText, pcenGainFromTime);
        ChangeText(_pcenFromTime, 0);
        ChangeText(_pcenFromMetal, 0);
        ChangeText(_pcenLost, 0);
    }
}
