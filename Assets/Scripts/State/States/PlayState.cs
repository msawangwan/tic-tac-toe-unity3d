using UnityEngine;
using System;
using System.Collections;

public class PlayState : IState {
    public bool isStateExecuting { get; private set; }
    public bool isStateExit { get; private set; }

    public PlayState() {
        isStateExecuting = true;
    }

    public void EnterState ( ) { }
    public void ExecuteState ( ) { }
    public event Action<StateBeginExitEvent> StartStateTransition;
}
