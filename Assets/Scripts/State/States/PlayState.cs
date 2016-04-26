using UnityEngine;
using System;
using System.Collections;

public class PlayState : IState {
    private GameRound round;

    public bool IsStateExecuting { get; private set; }
    public bool IsStateExit { get; private set; }

    private bool gameWonOrQuit;

    public PlayState( GameRound currentRound ) {
        IsStateExecuting = true;
        gameWonOrQuit = false;
    }

    public void EnterState ( ) {
        Logger.DebugToConsole ( "PlayState" , "EnterState" , "Entering state ... " );
    }

    public void ExecuteState ( ) {
        Logger.DebugToConsole ( "PlayState" , "ExecuteState" , "Executing state ... " );
    }

    public event Action<StateBeginExitEvent> StartStateTransition;

    private void HandleOnTerminatePlay ( ) {
        Debug.Log ( "[HandleOnTerminatePlay][HandleOnApplicationLoadComplete] Exiting state." );
        IState nextState = new EndOfGameState();
        IStateTransition transition = new LoadingTransition();
        StateBeginExitEvent exitEvent = new StateBeginExitEvent(nextState, transition);
        StartStateTransition ( exitEvent );
    }
}
