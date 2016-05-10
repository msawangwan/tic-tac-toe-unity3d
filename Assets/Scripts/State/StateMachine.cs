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
            state.RaiseStateChangeEvent += HandleOnStateTransition;          // add listener
            state.EnterState ( );
        }
    }

    public bool isExecuting { get; private set; }

    /* Set the first state the State Machine will run on start. */
    public void SetInitialState ( IState initialState ) {
        isExecuting = true;      
        newState = initialState;
    }

    // signature matches 'startstatetransition' event
    private void HandleOnStateTransition ( StateBeginExitEvent exitStateEvent ) {
        nextState = exitStateEvent.NextState;
        exitStateTransition = exitStateEvent.Transition;
    }

    private void Update ( ) {
        if ( isExecuting ) {                                                 // is the engine state machine running
            Assert.IsFalse ( state == null , "[StateMachine][Update] State Machine has no state!" );

            if ( exitStateTransition == null && state.IsStateExecuting ) {   // RTC the current state, return until done
                state.ExecuteState ( );
                return;
            }

            state.RaiseStateChangeEvent -= HandleOnStateTransition;

            if ( nextState == null ) {                                       // check if a nextState was passed, if not -- app was terminated            
                isExecuting = false;
            }

            if ( exitStateTransition != null ) {                             // if the transition variable has a transition, run 'transition exit current state' coroutine
                if ( exitStateTransition.HasTriggered == false ) {
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
        }
    }
}