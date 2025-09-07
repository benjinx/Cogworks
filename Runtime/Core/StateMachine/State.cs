[System.Serializable]
public class State<T>
{
    public enum StateType
    {
        Base,
        Overlay,
        Action
    }
    
    protected T stateMachine;

    public StateType stateType;

    public State(T stateMachine, StateType stateType)
    {
        this.stateMachine = stateMachine;
        this.stateType = stateType;
    }

    public virtual bool CanEnterState() => true;
    
    public virtual void OnEnter(){}
    
    public virtual void OnUpdate(){}
    
    public virtual void OnFixedUpdate() {}
    
    public virtual void OnExit(){}

    // Used for action states
    public virtual bool IsFinished() => false;
}
