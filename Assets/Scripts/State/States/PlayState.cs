using UnityEngine;
using System;
using System.Collections;

public class PlayState : IState {
    public bool isStateExecuting { get; private set; }
    public bool isStateExit { get; private set; }

    private bool gameWonOrQuit;

    public PlayState() {
        isStateExecuting = true;
        gameWonOrQuit = false;
    }

    public void EnterState ( ) {
        Logger.DebugToConsole ( "PlayState" , "EnterState" , "Entering state ... " );
    }

    public void ExecuteState ( ) {
        Logger.DebugToConsole ( "PlayState" , "ExecuteState" , "Entering state ... " );
    }

    public event Action<StateBeginExitEvent> StartStateTransition;
}
