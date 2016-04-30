using UnityEngine;
using System;
using System.Collections.Generic;

public class LoadNewRoundState : IState {
    private GameRound round;

    private Dictionary<int, bool> playerTypes; // < id, isHumanPlayer >

    private Ticker delay;

    private float numTicksToDelay;
    private float numDelayTicks;

    private bool hasGameLoaded = false;

    public bool IsStateExecuting { get; private set; }

    public LoadNewRoundState( float loadTime ) {
        IsStateExecuting = true;

        playerTypes = new Dictionary<int , bool> ( );

        playerTypes.Add ( 0 , true );
        playerTypes.Add ( 1 , false );

        delay = new Ticker ( .6f );
        numTicksToDelay = loadTime;
        numDelayTicks = 0;

        hasGameLoaded = false;
    }

    public void EnterState ( ) { }

    public void ExecuteState ( ) {
        if ( hasGameLoaded == true ) {
            IsStateExecuting = false;
            OnGameSetupCompleted ( );
        }

        if ( delay != null ) {
            delay.Timer ( );
            numDelayTicks = delay.TickCount;
        }
                
        if (numDelayTicks > numTicksToDelay && hasGameLoaded == false) {
            round = new GameRound( );
            delay = null;
            hasGameLoaded = true;
        }
    }

    public event Action<StateBeginExitEvent> RaiseStateChangeEvent;

    private void OnGameSetupCompleted ( ) {
        IState nextState = new RoundState( round );
        IStateTransition transition = new LoadingTransition();
        StateBeginExitEvent exitEvent = new StateBeginExitEvent(nextState, transition);

        if ( RaiseStateChangeEvent != null ) {
            RaiseStateChangeEvent ( exitEvent );
        }
    }
}
