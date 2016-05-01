using UnityEngine;
using System;
using System.Collections;

public class RoundState : IState {
    private GameRound round;

    public bool IsStateExecuting { get; private set; }

    private bool gameWonOrQuit;

    public RoundState( GameRound newRound ) {
        gameWonOrQuit = false;
        round = newRound;
        round.LoadPlayers ( );
        round.LoadTurns ( );
    }

    public void EnterState ( ) {
        Debug.Log ( "[RoundState][EnterState] Entering state ... starting a new round " );
        round.StartNewRound ( );
        IsStateExecuting = true;
    }

    public void ExecuteState ( ) {
        Debug.Log ( "[RoundState][ExecuteState] Executing state ...  " );
        if ( gameWonOrQuit == false ) {
            if ( round.IsGameOver == false )
                return;
            else
                gameWonOrQuit = true;
        }

        if ( gameWonOrQuit ) {
            Debug.Log ( "[RoundState][ExecuteState] Game over... notify of exit event! " );
            OnGameOver ( );
        }
    }

    public event Action<StateBeginExitEvent> RaiseStateChangeEvent;

    private void OnGameOver ( ) {
        Debug.Log ( "[PlayState][HandleOnTerminatePlay] Exiting state." );
        IState nextState = new EndOfGameState();
        IStateTransition transition = new EndGameTransition( round );
        StateBeginExitEvent exitEvent = new StateBeginExitEvent(nextState, transition);

        if ( RaiseStateChangeEvent != null ) {
            RaiseStateChangeEvent ( exitEvent );
        }
    }
}
