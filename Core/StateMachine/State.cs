
[System.Serializable]
public abstract class State
{
    public State currentSubState;

    public void OnStateEnter()
    {
        OnEnter();
    }

    protected virtual void OnEnter(){}

    public void OnStateUpdate()
    {
        OnUpdate();
    }
    
    protected virtual void OnUpdate(){}

    public void OnStateFixedUpdate()
    {
        OnFixedUpdate();
    }
    
    protected virtual void OnFixedUpdate() {}

    public void OnStateExit()
    {
        OnExit();
    }

    protected virtual void OnExit(){}

    public void TransitionStateToSubState(State newSubState)
    {
        currentSubState.OnStateExit();
        currentSubState = newSubState;
        currentSubState.OnStateEnter();
    }

    protected virtual void ActionCompleted(){}
}