using UnityEngine;
using System.Collections;

public class StateBeginExitEvent {
    public IState NextState { get; private set; }
    public IStateTransition Transition { get; private set; }

    public StateBeginExitEvent(IState nextState, IStateTransition transition) {
        NextState = nextState;
        Transition = transition;
    }
}
