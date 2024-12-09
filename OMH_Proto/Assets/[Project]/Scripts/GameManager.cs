using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public enum GameState
{
    NotSet,
    Exploration,
    Defense,
    Victory,
}

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameEvent _defenseStartEvent,
        _victoryGameEvent,
        _defenseAlmostEndEvent,
        _explorationAlmostEndEvent,
        _explorationMidEvent,
        _explorationStartEvent;
    [Space]
    [SerializeField] private FloatReference _gameTime;
    [SerializeField] private FloatReference _explorationDuration, _defenseDuration;
    [SerializeField] private NavMeshSurface _navMesh;
    private GameState _currentGameState;

    private void Start()
    {
        SetGameState(GameState.Exploration);
        _explorationStartEvent.Raise();
        _gameTime.Value = 0;
    }

    private void Update()
    {
        _gameTime.Value += Time.deltaTime;
        if (_gameTime.Value > _defenseDuration.Value && _currentGameState == GameState.Defense)
        {
            SetGameState(GameState.Victory);
        }
        else if (_gameTime.Value > _explorationDuration.Value && _currentGameState == GameState.Exploration)
        {
            SetGameState(GameState.Defense);
            _gameTime.Value = 0;
        }
        
        if (_defenseDuration.Value - _gameTime.Value <= 10  && _currentGameState == GameState.Defense)
        {
            _defenseAlmostEndEvent.Raise();
        }
        if (_explorationDuration.Value - _gameTime.Value <= 30  && _currentGameState == GameState.Exploration)
        {
            _explorationAlmostEndEvent.Raise();
        }
        if (_gameTime.Value >= _explorationDuration.Value / 2 && _currentGameState == GameState.Exploration)
        {
            _explorationMidEvent.Raise();
        }
    }

    private void SetGameState(GameState toSet)
    {
        if (_currentGameState == toSet) return;
        _currentGameState = toSet;
        switch (_currentGameState)
        {
            case GameState.Exploration:
                //! ????
                break;

            case GameState.Defense:
                _defenseStartEvent.Raise();
                break;
            case GameState.Victory:
                _victoryGameEvent.Raise();
                break;
        }
    }

    public void RebakeNavmesh()
    {
        _navMesh.UpdateNavMesh(_navMesh.navMeshData);
    }
}
