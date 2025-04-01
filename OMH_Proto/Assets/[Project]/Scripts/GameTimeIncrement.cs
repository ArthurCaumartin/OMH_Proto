using UnityEngine;

public class GameTimeIncrement : MonoBehaviour
{
    public FloatReference _gameTime;

    private void Update()
    {
        _gameTime.Value += Time.deltaTime;
    }
}