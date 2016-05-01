using UnityEngine;
using System;
using System.Collections;

public class RoundState : IState {
    public bool IsStateExecuting { get; private set; }

    private GameRound round;
    private bool gameWonOrQuit;

    /* Constructor. */
    public RoundState( GameRound newRound ) {
        gameWonOrQuit = false;
        round = newRound;
    }

    public void EnterState ( ) {
        round.LoadPlayers ( );
        round.LoadTurns ( );
        round.StartNewRound ( );

        IsStateExecuting = true;
    }

    public void ExecuteState ( ) {
        if ( gameWonOrQuit == false ) {
            if ( round.IsGameOver == false )
                return;
            else
                gameWonOrQuit = true;
        }

        if ( gameWonOrQuit ) {
            OnGameOver ( );
        }
    }

    public event Action<StateBeginExitEvent> RaiseStateChangeEvent;

    private void OnGameOver ( ) {
        IState nextState = new RoundEndState( round );
        IStateTransition transition = new LoadingTransition( round.LoadedTransitionOutroAsset );
        StateBeginExitEvent exitEvent = new StateBeginExitEvent(nextState, transition);

        if ( RaiseStateChangeEvent != null ) {
            RaiseStateChangeEvent ( exitEvent );
        }
    }
}
