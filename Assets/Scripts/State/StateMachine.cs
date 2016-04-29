using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;

public class StateMachine : MonoBehaviour, IStateMachine  {
    private IStateTransition exitStateTransition;

    private IState state;
    private IState nextState;  
    private IState newState {
        set {
            state = value;
            Debug.Log ( "[StateMachine][newState] Adding new state as listener ... " );
            state.RaiseStateChangeEvent += HandleOnStateTransition;          // add listener
            state.EnterState ( );
        }
    }

    public bool isExecuting { get; private set; }

    // use as constructor
    public void InitStateMachine ( IState initialState ) {
        isExecuting = true;      
        newState = initialState;
        Debug.Log ( "[StateMachine][InitStateMachine] State Machine Initialised. " );
    }

    // signature matches 'startstatetransition' event
    private void HandleOnStateTransition ( StateBeginExitEvent exitStateEvent ) {
        Debug.Log ( "[StateMachine][HandleStateExit] Exit Event Fired. " );
        nextState = exitStateEvent.NextState;
        exitStateTransition = exitStateEvent.Transition;
    }

    private void Update ( ) {
        if ( isExecuting ) {                                                 // is the engine state machine running
            Assert.IsFalse ( state == null , "[StateMachine][Update] State Machine has no state!" );
            Debug.Log ( "[StateMachine][Update] Current State: " + state.GetType ( ) );

            if ( exitStateTransition == null && state.IsStateExecuting ) {   // RTC the current state, return until done
                Debug.Log ( "[StateMachine][Update] Executing state ... " );
                state.ExecuteState ( );
                return;
            }

            Debug.Log ( "[StateMachine][Update] Removing current state listener ... " );
            state.RaiseStateChangeEvent -= HandleOnStateTransition;

            if ( nextState == null ) {                                       // check if a nextState was passed, if not -- app was terminated
                Debug.Log ( "[StateMachine][Update] Application terminating ... " );             
                isExecuting = false;
            }

            if ( exitStateTransition != null ) {                             // if the transition variable has a transition, run 'transition exit current state' coroutine
                if ( exitStateTransition.HasTriggered == false ) {
                    Debug.Log ( "[StateMachine][Update] Triggering exit transition to next state ... " );
                    StartCoroutine ( exitStateTransition.BeginTransition ( ).GetEnumerator ( ) );
                }

                if ( exitStateTransition.HasCompleted == false ) {
                    return;
                }
            }

            // TODO: consider adding a state.EndExit()

            newState = nextState;                                            // this fires the property, and the new state will run its 'enter' method
            nextState = null;

            // TODO: add a 'transition enter next state'

            exitStateTransition = null;

            // TODO: add a state.EndEnter();
            Debug.Log ( "[StateMachine][Update] Last call in update ... " );
        }
    }
}