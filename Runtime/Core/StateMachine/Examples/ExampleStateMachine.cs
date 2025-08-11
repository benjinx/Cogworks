public class ExampleStateMachine : StateMachine
{
    ExampleState exampleState = new ExampleState();

    protected override void Start()
    {
        exampleState.exampleStateMachine = this;
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
