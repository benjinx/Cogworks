using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [HideInInspector]
    public State previousState;

    [HideInInspector]
    public State currentState;

    protected virtual void Start()
    {
        
    }

    protected virtual void Update()
    {
        if (currentState != null)
        {
            currentState.OnStateUpdate();
        }
    }

    protected virtual void FixedUpdate()
    {
        if (currentState != null)
        {
            currentState.OnStateFixedUpdate();
        }
    }

    public void ChangeState(State newState)
    {
        if (currentState != null)
        {
            currentState.OnStateExit();
        }

        previousState = currentState;
        currentState = newState;
        currentState.OnStateEnter();
    }
}
