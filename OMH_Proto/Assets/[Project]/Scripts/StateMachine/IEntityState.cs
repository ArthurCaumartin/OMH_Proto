public interface IEntityState
{
    public void Initialize(StateMachine behavior);

    public void EnterState(StateMachine behavior);

    public void DoState(StateMachine behavior);

    public void ExitState(StateMachine behavior);
}
