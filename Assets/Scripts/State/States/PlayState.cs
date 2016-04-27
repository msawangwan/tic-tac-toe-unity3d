using UnityEngine;
using System;
using System.Collections;

public class PlayState : IState {
    private GameRound currentRound;

    public bool IsStateExecuting { get; private set; }

    private bool gameWonOrQuit;

    public PlayState( GameRound newRound ) {
        currentRound = newRound;
        IsStateExecuting = true;
        gameWonOrQuit = false;
    }

    public void EnterState ( ) {
        Debug.Log ( "[PlayState][EnterState] Entering state ... starting a new round " );
        currentRound.StartNewRound ( currentRound );
    }

    public void ExecuteState ( ) {
        Debug.Log ( "[PlayState][ExecuteState] Executing state ...  " );
        if ( gameWonOrQuit == false ) {
            if ( currentRound.IsGameOver == false )
                return;
            else
                gameWonOrQuit = true;
        }

        if ( gameWonOrQuit ) {
            Debug.Log ( "[PlayState][ExecuteState] Game over... notify of exit event! " );
            HandleOnTerminatePlay ( );
        }
    }

    public event Action<StateBeginExitEvent> StartStateTransition;

    private void HandleOnTerminatePlay ( ) {
        Debug.Log ( "[PlayState][HandleOnTerminatePlay] Exiting state." );
        IState nextState = new EndOfGameState();
        IStateTransition transition = new LoadingTransition();
        StateBeginExitEvent exitEvent = new StateBeginExitEvent(nextState, transition);
        StartStateTransition ( exitEvent );
    }
}
