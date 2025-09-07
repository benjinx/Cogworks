using UnityEngine;

public class ExampleBaseState : ExampleState
{
    public ExampleBaseState(ExampleStateMachine stateMachine) : base(stateMachine, StateType.Base)
    {
    }

    public override bool CanEnterState() => true;
    
    public override void OnEnter(){}
    
    public override void OnUpdate(){}
    
    public override void OnFixedUpdate() {}
    
    public override void OnExit(){}
}
