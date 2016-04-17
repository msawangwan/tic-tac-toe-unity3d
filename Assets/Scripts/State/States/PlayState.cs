using UnityEngine;
using System;
using System.Collections;

public class PlayState : IState {
    public void EnterState ( ) { }
    public void ExecuteState ( ) { }
    public event Action<StateBeginExitEvent> StartStateTransition;
}
