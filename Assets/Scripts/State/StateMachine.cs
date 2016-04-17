using UnityEngine;
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

    // to do, account for transition
    private void Update ( ) {
        if (state == null) {
            Debug.Log ( "[StateMachine][Update] No State Set ... " );
            return;
        }
        Debug.Log ( "[StateMachine][Update] Executing state ... " + state.GetType() );
        state.ExecuteState ( );
        if (state == null) {
            return;
        }
        state.StartStateTransition -= HandleStateExit;
    }

    public void InitStateMachine(IState initialState) {
        InternalState = initialState;
        //state.EndEnter();
    }

    public void HandleStateExit( StateBeginExitEvent exitStateEvent ) {
        nextState = exitStateEvent.NextState;
        transition = exitStateEvent.Transition;
    }
}