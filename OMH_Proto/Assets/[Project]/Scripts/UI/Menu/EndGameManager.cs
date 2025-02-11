using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndGameManager : MonoBehaviour
{
    [SerializeField] private FloatReference _syphonHealth, _pcen, _scrapMetal;
    [SerializeField] private FloatReference _gameTime, _defenseDuration, explorationDuration;
    [Space]
    [SerializeField] private PlayerItemList _playerItemList;
    [Space]
    [SerializeField] private TextMeshProUGUI _pcenText, _titleText;
    [SerializeField] private TextMeshProUGUI _pcenFromTime, _pcenFromMetal;

    [SerializeField] private float _costEachHealthLost = 2;
    
    void Start()
    {
        if (_syphonHealth.Value <= 0)
        {
            LostGame();
        }
        else
        {
            WinGame();
        }
    }

    private void LostGame()
    {
        _titleText.text = "You lost !";
        float defenseTimeBeforeLost = _defenseDuration.Value - (_gameTime.Value - explorationDuration.Value);
        
        int pcenGainFromTime = Mathf.RoundToInt(defenseTimeBeforeLost * 10);
        int pcenGainFromMetal =  Mathf.RoundToInt(_scrapMetal.Value / 10);
        int pcenLostFromSyphonHealth = (int)(20 * _costEachHealthLost);
    }

    private void WinGame()
    {
        _titleText.text = "You won !";

        int pcenGainFromTime = Mathf.RoundToInt( _defenseDuration.Value);
        int pcenGainFromMetal =  Mathf.RoundToInt(_scrapMetal.Value / 10);
        int pcenLostFromSyphonHealth = (int)(_syphonHealth.Value * _costEachHealthLost);
    }
}
