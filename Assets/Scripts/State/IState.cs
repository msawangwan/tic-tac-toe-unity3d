using UnityEngine;
using System;
using System.Collections;

public interface IState {
    bool IsStateExecuting { get; } // default to 'true' in constructor or 'EnterState'

    void EnterState ( );
    void ExecuteState ( );

    event Action<StateBeginExitEvent> RaiseStateChangeEvent;
}