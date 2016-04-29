using UnityEngine;
using System;
using System.Collections;

public class RoundState : IState {
    private IRound currentRound;

    public bool IsStateExecuting { get; private set; }

    private bool gameWonOrQuit;

    public RoundState( IRound newRound ) {
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
            OnGameOver ( );
        }
    }

    public event Action<StateBeginExitEvent> RaiseStateChangeEvent;

    private void OnGameOver ( ) {
        Debug.Log ( "[PlayState][HandleOnTerminatePlay] Exiting state." );
        IState nextState = new EndOfGameState();
        IStateTransition transition = new EndGameTransition( currentRound );
        StateBeginExitEvent exitEvent = new StateBeginExitEvent(nextState, transition);

        if ( RaiseStateChangeEvent != null ) {
            RaiseStateChangeEvent ( exitEvent );
        }
    }
}
