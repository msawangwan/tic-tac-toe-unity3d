using UnityEngine;
using System;
using System.Collections;

public class LoadNewGameState : IState {
    private Utility delayTimer;

    private float numTicksToDelay;
    private float numDelayTicks;

    private bool hasGameLoaded = false;

    public bool isStateExecuting { get; private set; }
    public bool isStateExit { get; private set; }

    public LoadNewGameState( float loadTime ) {
        isStateExecuting = true;
        delayTimer = new Utility ( 1.7f );
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
            isStateExecuting = false;
            HandleOnNewGameLoadComplete ( );
        }

        if ( delayTimer != null ) {
            delayTimer.Timer ( );
            numDelayTicks = delayTimer.timer_tickCount;
        }
                
        if (numDelayTicks > numTicksToDelay && hasGameLoaded == false) {
            Logger.DebugToConsole ( "LoadNewGameState" , "ExecuteState" , "Starting new round ..." );
            GameRound newRound = new GameRound();
            delayTimer = null;
            hasGameLoaded = true;
        }
    }

    public event Action<StateBeginExitEvent> StartStateTransition;

    private void HandleOnNewGameLoadComplete ( ) {
        Logger.DebugToConsole ( "LoadNewGameState" , "HandleOnNewGameLoadComplete" , "Exiting state." );
        IState nextState = new PlayState();
        IStateTransition transition = new ExitLoadingTransition();
        StateBeginExitEvent exitEvent = new StateBeginExitEvent(nextState, transition);
        StartStateTransition ( exitEvent );
    }
}
