using UnityEngine;
using System;
using System.Collections;

public interface IState {
    void EnterState ( );
    void ExecuteState ( );
    event Action<StateBeginExitEvent> StartStateTransition;
}