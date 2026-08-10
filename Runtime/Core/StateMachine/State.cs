[System.Serializable]
public class State<T>
{
    protected T stateMachine;

    public State(T stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public virtual bool CanEnterState() => true;
    
    public virtual bool CanExitState() => true;

    public virtual void OnEnter(){}
    
    public virtual void OnUpdate(){}
    
    public virtual void OnFixedUpdate() {}
    
    public virtual void OnExit(){}
}
