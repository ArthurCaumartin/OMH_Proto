using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private FloatReference _gameTime;
    [SerializeField] private FloatReference _explorationDuration;

    private void Start()
    {
        _gameTime.Value = 0;
    }

    private void Update()
    {
        _gameTime.Value += Time.deltaTime;
        if(_gameTime.Value > _explorationDuration.Value)
        {
            print("TRIGGER DEFENSE PHASE");
        }
    }
}
