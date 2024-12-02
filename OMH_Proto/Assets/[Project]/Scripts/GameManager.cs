using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public enum GameState
{
    NotSet,
    Exploration,
    Defense,
}

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameEvent _defenseStartEvent;
    [Space]
    [SerializeField] private FloatReference _gameTime;
    [SerializeField] private FloatReference _explorationDuration;
    [SerializeField] private NavMeshSurface _navMesh;
    private GameState _currentGameState;

    private void Start()
    {
        _gameTime.Value = 0;
    }

    private void Update()
    {
        _gameTime.Value += Time.deltaTime;
        if (_gameTime.Value > _explorationDuration.Value)
        {
            // print("Set Defense !");
            SetGameState(GameState.Defense);
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
        }
    }

    public void RebakeNavmesh()
    {
        _navMesh.UpdateNavMesh(_navMesh.navMeshData);
    }
}
