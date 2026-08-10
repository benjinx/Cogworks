using UnityEngine;

public class StateMachine<TState, TStateMachine> : MonoBehaviour
    where TState : State<TStateMachine> // Tells us TState is any type of State that desires a state machine type
    where TStateMachine : StateMachine<TState, TStateMachine> // Tells us TStateMachine is a state machine with a TState and TStateMachine
{
    public TState currentState { get; private set; }

    public void Initialize(TState startingState)
    {
        if (startingState == null)
        {
            return;
        }

        if (!startingState.CanEnterState())
        {
            return;
        }
        
        currentState = startingState;
        currentState.OnEnter();
    }
    
    protected virtual void Update()
    {
        currentState?.OnUpdate();
    }

    protected virtual void FixedUpdate()
    {
        currentState?.OnFixedUpdate();
    }

    public bool TryEnterState(TState newState)
    {
        if (newState == null)
        {
            return false;
        }

        if (currentState == newState)
        {
            return false;
        }

        if (!newState.CanEnterState())
        {
            return false;
        }

        if (!currentState.CanExitState())
        {
            return false;
        }
        
        currentState?.OnExit();
        currentState = newState;
        currentState?.OnEnter();

        return true;
    }
}
