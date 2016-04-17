using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;

public class StateMachine : MonoBehaviour, IStateMachine  {
    private IStateTransition transition;
    private IState state;
    private IState nextState;  
    private IState InternalState {
        set {
            state = value;
            state.StartStateTransition += HandleStateExit;
            state.EnterState ( );
        }
    }

    public bool isStateMachineExecuting { get; private set; }

    // use as constructor
    public void InitStateMachine ( IState initialState ) {
        isStateMachineExecuting = true;
        Debug.Log ( "[StateMachine][InitStateMachine] State Machine Initialised. " );
        InternalState = initialState;
        // TODO: add a state.EndEnter();
    }

    // fires event and gets values for next state
    public void HandleStateExit ( StateBeginExitEvent exitStateEvent ) {
        Debug.Log ( "[StateMachine][HandleStateExit] Exit Event Fired. " );
        nextState = exitStateEvent.NextState;
        transition = exitStateEvent.Transition;
    }

    private void Update ( ) {
        if ( isStateMachineExecuting ) {
            Assert.IsFalse ( state == null , "[StateMachine][Update] State Machine has no state!" );
            Debug.Log ( "[StateMachine][Update] Current State: " + state.GetType ( ) );

            if ( transition == null && state.isStateExecuting ) {
                Debug.Log ( "[StateMachine][Update] Executing state ... " );
                state.ExecuteState ( );
                return;
            }

            state.StartStateTransition -= HandleStateExit;

            if ( nextState == null ) { // if no next state, then we've quit the application
                Debug.Log ( "[StateMachine][Update] Application terminating ... " );             
                isStateMachineExecuting = false; // TODO: add a app should quit in a more elegant manner
            }

            if ( transition != null ) {
                if ( transition.hasTriggered == false ) {
                    Debug.Log ( "[StateMachine][Update] Triggering exit transition to next state ... " );
                    StartCoroutine ( transition.Exit ( ).GetEnumerator ( ) );
                }

                if ( transition.hasCompleted == false ) {
                    return;
                }
            }

            // TODO: add a state.EndExit()

            InternalState = nextState;
            nextState = null;

            // TODO: add a run enter transition

            transition = null;
            Debug.Log ( "[StateMachine][Update] Last call in update ... " );

            // TODO: add a state.EndEnter();
        }
    }
}