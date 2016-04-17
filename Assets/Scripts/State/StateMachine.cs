using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;

public class StateMachine : MonoBehaviour, IStateMachine  {
    private bool isStateComplete = true;

    private IStateTransition transition;
    private IState state;
    private IState nextState;  
    private IState InternalState {
        set {
            state = value;
            state.StartStateTransition += HandleStateExit;
            state.EnterState ( );
            isStateComplete = false;
        }
    }

    private void Update ( ) {
        Assert.IsFalse ( state == null , "[StateMachine][Update] State Machine has no state!" );
        Debug.Log ( "[StateMachine][Update] Current State: " + state.GetType ( ) );

        if ( transition == null ) {
            Debug.Log ( "[StateMachine][Update] Executing state ... " );
            state.ExecuteState ( );            
            return;
        }

        Debug.Log ( "[StateMachine][Update] Preparing to transition to next state ... " );
        state.StartStateTransition -= HandleStateExit;

        if (nextState == null) {
            Debug.Log ( "[StateMachine][Update] Application terminating ... " );
            // means app should quit
        }

        InternalState = nextState;
        nextState = null;

        if ( transition == null ) {
            Debug.Log ( "[StateMachine][Update] transition2 ... " );
            return;
        }

        transition = null;
        Debug.Log ( "[StateMachine][Update] Last call in update ... " );
    }

    public void InitStateMachine(IState initialState) {
        Debug.Log ( "[StateMachine][InitStateMachine] State Machine Initialised. " );
        InternalState = initialState;
        //state.EndEnter();
    }

    public void HandleStateExit( StateBeginExitEvent exitStateEvent ) {
        Debug.Log ( "[StateMachine][HandleStateExit] Exit Event Fired. " );
        nextState = exitStateEvent.NextState;
        transition = exitStateEvent.Transition;
    }
}