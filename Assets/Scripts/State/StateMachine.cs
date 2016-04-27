using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;

public class StateMachine : MonoBehaviour, IStateMachine  {
    private IStateTransition transition;

    private IState state;
    private IState nextState;  
    private IState newState {
        set {
            state = value;
            Debug.Log ( "[StateMachine][newState] Adding state as listener ... " );
            state.StartStateTransition += HandleStateExit; // add listener
            state.EnterState ( );
        }
    }

    public bool isExecuting { get; private set; }

    // use as constructor
    public void InitStateMachine ( IState initialState ) {
        isExecuting = true;      
        newState = initialState;
        Debug.Log ( "[StateMachine][InitStateMachine] State Machine Initialised. " );
        // TODO: add a state.EndEnter();
    }

    // fires event and notifies any listeners
    public void HandleStateExit ( StateBeginExitEvent exitStateEvent ) {
        Debug.Log ( "[StateMachine][HandleStateExit] Exit Event Fired. " );
        nextState = exitStateEvent.NextState;
        transition = exitStateEvent.Transition;
    }

    private void Update ( ) {
        if ( isExecuting ) {
            Assert.IsFalse ( state == null , "[StateMachine][Update] State Machine has no state!" );
            Debug.Log ( "[StateMachine][Update] Current State: " + state.GetType ( ) );

            if ( transition == null && state.IsStateExecuting ) {
                Debug.Log ( "[StateMachine][Update] Executing state ... " );
                state.ExecuteState ( );
                return;
            }

            Debug.Log ( "[StateMachine][Update] Removing state as listener ... " );
            state.StartStateTransition -= HandleStateExit;

            if ( nextState == null ) { // if no next state, then we've quit the application
                Debug.Log ( "[StateMachine][Update] Application terminating ... " );             
                isExecuting = false; // TODO: add a app should quit in a more elegant manner
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

            newState = nextState;
            nextState = null;

            // TODO: add a run enter transition

            transition = null;
            Debug.Log ( "[StateMachine][Update] Last call in update ... " );

            // TODO: add a state.EndEnter();
        }
    }
}