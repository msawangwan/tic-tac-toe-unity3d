using UnityEngine;
using System;
using System.Collections;

public interface IState {
    bool isStateExecuting { get; } // sets to 'true' in constructor
    bool isStateExit { get; }      // sets to 'false' in constructor

    void EnterState ( );
    void ExecuteState ( );

    event Action<StateBeginExitEvent> StartStateTransition;
}