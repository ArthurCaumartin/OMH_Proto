public interface IEntityState
{
    public void Initialize(StateMachine behavior);

    public void EnterState();

    public void UpdateState();

    public void ExitState();
}
