using UnityEngine;
using System;
using System.Collections;

public interface IState {
    bool isStateExecuting { get; }
    bool isStateExit { get; }

    void EnterState ( );
    void ExecuteState ( );

    event Action<StateBeginExitEvent> StartStateTransition;
}