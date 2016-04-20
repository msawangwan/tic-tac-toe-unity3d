using UnityEngine;
using System;
using System.Collections;

public class EndOfGameState : IState {
    public bool isStateExecuting { get; private set; } // sets to 'true' in constructor
    public bool isStateExit { get; private set; }      // sets to 'false' in constructor

    public void EnterState ( ) { }
    public void ExecuteState ( ) { }

    public event Action<StateBeginExitEvent> StartStateTransition;
}
