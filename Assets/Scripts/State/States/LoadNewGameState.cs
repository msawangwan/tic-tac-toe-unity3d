using UnityEngine;
using System;
using System.Collections;

public class LoadNewGameState : IState {
    private GameRound newRound;

    private Utility delayTimer;

    private float numTicksToDelay;
    private float numDelayTicks;

    private bool hasGameLoaded = false;

    public bool IsStateExecuting { get; private set; }

    public LoadNewGameState( float loadTime ) {
        IsStateExecuting = true;
        delayTimer = new Utility ( .6f );
        numTicksToDelay = loadTime;
        numDelayTicks = 0;

        hasGameLoaded = false;
    }

    public void EnterState ( ) {
        Logger.DebugToConsole ( "LoadNewGameState" , "EnterState" , "Entering state ... " );
    }

    public void ExecuteState ( ) {
        Logger.DebugToConsole ( "LoadNewGameState" , "ExecuteState" , "Executing ... " );
        if ( hasGameLoaded == true ) {
            IsStateExecuting = false;
            OnGameSetupComplete ( );
        }

        if ( delayTimer != null ) {
            delayTimer.Timer ( );
            numDelayTicks = delayTimer.timer_tickCount;
        }
                
        if (numDelayTicks > numTicksToDelay && hasGameLoaded == false) {
            Logger.DebugToConsole ( "LoadNewGameState" , "ExecuteState" , "Starting new round ..." );
            newRound = GameRound.LoadNewRound ( );
            delayTimer = null;
            hasGameLoaded = true;
        }
    }

    public event Action<StateBeginExitEvent> RaiseStateChangeEvent;

    private void OnGameSetupComplete ( ) {
        Logger.DebugToConsole ( "LoadNewGameState" , "HandleOnNewGameLoadComplete" , "Exiting state." );
        IState nextState = new PlayState( newRound );
        IStateTransition transition = new LoadingTransition(); // <- TODO: change to 'startnewgametransition' for boardgame fade-in
        StateBeginExitEvent exitEvent = new StateBeginExitEvent(nextState, transition);
        RaiseStateChangeEvent ( exitEvent );
    }
}
