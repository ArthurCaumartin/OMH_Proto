using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndGameManager : MonoBehaviour
{
    [SerializeField] private GameChooseMeta _gameChooseMeta;
    [SerializeField] private FloatReference _syphonHealth, _pcen, _kills, _defenses, _dangerLevel;
    [SerializeField] private FloatReference _gameTime, _defenseDuration, explorationDuration;
    [Space] [SerializeField] private PlayerItemList _playerItemList;
    [SerializeField] private ObjectUIManager _objectUIManager;
    [Space] [SerializeField] private TextMeshProUGUI _titleText, _lostHpText, _timerGameText, _dangerLevelText;
    [SerializeField] private TextMeshProUGUI _countDefenses, _countKills;
    [SerializeField] private GameObject _total;

    [Space]
    
    [SerializeField, Tooltip("pcen lost each hp lost")] private float _costEachHealthLost = 2;
    [SerializeField, Tooltip("pcen gain each defend second")] private float _valueEachSecond = 10;
    [SerializeField, Tooltip("NumberMetals / x pcen to gain")] private float _valueDivisionMetal = 10;
    
    void Start()
    {
        //Items
        for (int i = 0; i < _playerItemList._items.Count; i++)
        {
            ItemScriptable tempItem = _playerItemList._items[i];
            _objectUIManager.AddObjectUI(tempItem._itemName, tempItem._itemDescription, tempItem._itemSprite);
        }
        
        //Danger Level
        _dangerLevelText.text = $"- {_dangerLevel.Value} -";

        //HP
        _lostHpText.text = $"- {20 - _syphonHealth.Value} -";
        _countDefenses.text = $"- {_defenses.Value} -";
        _countKills.text = $"- {_kills.Value} -";
        
        //Timer
        int timerMinute = Mathf.RoundToInt(_gameTime.Value / 60);
        string timerMinuteString = "" + timerMinute;
        if (timerMinute < 10)
        {
            timerMinuteString = "0" + timerMinute;
        }
        int timerSeconds = Mathf.RoundToInt(_gameTime.Value % 60);
        string timerSecondsString = "" + timerSeconds;
        if (timerMinute < 10)
        {
            timerSecondsString = "0" + timerMinute;
        }
        _timerGameText.text = $"-{timerMinuteString}:{timerSecondsString}-";
        
        //Choose lost or win
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
        _titleText.text = "Mission failed";
        

        float defenseTimeBeforeLost = _gameTime.Value;
        int pcenGainFromTime = Mathf.RoundToInt(defenseTimeBeforeLost * _valueEachSecond);
        
        SetupValues(pcenGainFromTime);
    }

    public void WinGame()
    {
        _titleText.text = "Succes !";

        int pcenGainFromTime = Mathf.RoundToInt(_defenseDuration.Value * _valueEachSecond);
        
        SetupValues(pcenGainFromTime);
    }

    private void SetupValues(int pcenTime)
    {
        ChangeText(_total, pcenTime);
        // StartCoroutine(AddPcen(pcenTime));
    }

    private void ChangeText(GameObject textObject, int value)
    {
        textObject.GetComponent<UpdateTextAnimation>().ChangeTextAnimation(value);
    }

    // private IEnumerator AddPcen(int pcenTime)
    // {
    //     int pcenGainFromMetal = Mathf.RoundToInt(_scrapMetal.Value / _valueDivisionMetal);
    //     int pcenLostFromSyphonHealth = (int)((20 - _syphonHealth.Value) * _costEachHealthLost);
    //     
    //     _pcen.Value = pcenTime + pcenGainFromMetal + pcenLostFromSyphonHealth;
    //     
            // yield return new WaitForSeconds(2);
    //     
    //     ChangeText(_countDefenses, pcenTime);
    //     yield return new WaitForSeconds(2);
    //     
    //     ChangeText(_countHP, pcenGainFromMetal);
    //
    //     if (pcenLostFromSyphonHealth != 0)
    //     {
    //         yield return new WaitForSeconds(2);
    //         ChangeText(_countKills, pcenLostFromSyphonHealth);
    //     }
    //     
    //     yield return new WaitForSeconds(2);
    //     
    //     ChangeText(_countDefenses, 0);
    //     ChangeText(_total, pcenTime);
    //     yield return new WaitForSeconds(2);
    //     
    //     ChangeText(_countHP, 0);
    //     ChangeText(_total, pcenGainFromMetal + pcenTime);
    //     yield return new WaitForSeconds(2);
    //     
    //     ChangeText(_countKills, 0);
    // }
}
