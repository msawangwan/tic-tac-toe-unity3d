using UnityEngine;
using System;
using System.Collections;

public interface IState {
    bool IsStateExecuting { get; } // sets to 'true' in constructor
    bool IsStateExit { get; }      // sets to 'false' in constructor

    void EnterState ( );
    void ExecuteState ( );

    event Action<StateBeginExitEvent> StartStateTransition;
}