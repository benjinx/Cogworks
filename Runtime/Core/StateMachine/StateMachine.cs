using UnityEngine;

public class StateMachine<TState, TStateMachine> : MonoBehaviour
    where TState : State<TStateMachine> // Tells us TState is any type of State that desires a state machine type
    where TStateMachine : StateMachine<TState, TStateMachine> // Tells us TStateMachine is a state machine with a TState and TStateMachine
{
    public TState currentBaseState { get; private set; }
    
    public TState currentOverlayState { get; private set; }
    
    public TState currentActionState { get; private set; }

    public void Initialize(TState startingState)
    {
        currentBaseState = startingState;
        currentBaseState.OnEnter();
    }
    
    protected virtual void Update()
    {
        if (currentActionState != null)
        {
            currentActionState.OnUpdate();

            if (currentActionState.IsFinished())
            {
                currentActionState.OnExit();
                currentActionState = null;
            }

            return; // don't process base/overlay while actions run
        }
        
        currentOverlayState?.OnUpdate();
        currentBaseState?.OnUpdate();
    }

    protected virtual void FixedUpdate()
    {
        if (currentActionState != null)
        {
            currentActionState.OnFixedUpdate();

            if (currentActionState.IsFinished())
            {
                currentActionState.OnExit();
                currentActionState = null;
            }

            return; // don't process base/overlay while actions run
        }
        
        currentOverlayState?.OnFixedUpdate();
        currentBaseState?.OnFixedUpdate();
    }

    public void ChangeBaseState(TState newBaseState)
    {
        if (newBaseState == null)
        {
            currentBaseState?.OnExit();
            currentBaseState = newBaseState;
            return;
        }
        
        if (newBaseState.stateType == State<TStateMachine>.StateType.Base)
        {
            currentBaseState?.OnExit();
            currentBaseState = newBaseState;
            currentBaseState?.OnEnter();
        }
    }

    public void ChangeOverlayState(TState newOverlayState)
    {
        if (newOverlayState == null)
        {
            currentOverlayState?.OnExit();
            currentOverlayState = newOverlayState;
            return;
        }
        
        if (newOverlayState.stateType == State<TStateMachine>.StateType.Overlay)
        {
            currentOverlayState?.OnExit();
            currentOverlayState = newOverlayState;
            currentOverlayState?.OnEnter();
        }
    }

    public void ChangeActionState(TState newActionState)
    {
        if (newActionState == null)
        {
            currentActionState?.OnExit();
            currentActionState = newActionState;
            return;
        }
        
        if (newActionState.stateType == State<TStateMachine>.StateType.Action)
        {
            currentActionState?.OnExit();
            currentActionState = newActionState;
            currentActionState?.OnEnter();
        }
    }
}
