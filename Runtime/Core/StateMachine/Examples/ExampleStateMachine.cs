using System;

public class ExampleStateMachine : StateMachine<ExampleState, ExampleStateMachine>
{
    private ExampleState exampleState;

    public void Awake()
    {
        exampleState = new ExampleBaseState(this);
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
