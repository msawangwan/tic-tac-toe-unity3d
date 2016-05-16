using UnityEngine;
using System;
using System.Collections;

public class RoundState : IState {
    public bool IsStateExecuting { get; private set; }

    private GameRound round;
    private bool gameWonOrQuit;

    private MusicMasterController musicplayer;

    /* Constructor. */
    public RoundState( GameRound newRound ) {
        gameWonOrQuit = false;
        round = newRound;
        musicplayer = MonoBehaviour.FindObjectOfType<MusicMasterController> ( );
    }

    public void EnterState ( ) {
        round.LoadPlayers ( );
        round.StartNewRound ( );

        IsStateExecuting = true;
    }

    public void ExecuteState ( ) {
        if ( gameWonOrQuit == false ) {
            if ( round.IsGameOver == false ) {
                round.Game.PlayTicTacToe ( );
                if (round.Game.IsGameover == true) {
                    gameWonOrQuit = true;
                }
                return;
            }
        }

        if ( gameWonOrQuit ) {
            round.Game.DestroyPlayers ( );
            OnGameOver ( );
        }
    }

    public event Action<StateBeginExitEvent> RaiseStateChangeEvent;

    private void OnGameOver ( ) {
        IState nextState = new RoundEndState( round );
        IStateTransition transition = new LoadingTransition( round.LoadedTransitionOutroAsset );
        StateBeginExitEvent exitEvent = new StateBeginExitEvent(nextState, transition);

        musicplayer.MusicCheck ( true );

        if ( RaiseStateChangeEvent != null )
            RaiseStateChangeEvent ( exitEvent );
    }
}
