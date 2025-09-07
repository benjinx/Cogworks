public abstract class ExampleState : State<ExampleStateMachine>
{
    protected ExampleState(ExampleStateMachine stateMachine, StateType stateType) : base(stateMachine, stateType)
    {
    }
}