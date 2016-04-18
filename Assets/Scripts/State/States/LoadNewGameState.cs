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
        delayTimer = new Utility ( 2.2f );
        numTicksToDelay = loadTime;
        numDelayTicks = 0;

        hasGameLoaded = false;
    }

    public void EnterState ( ) {
        Debug.Log ( "[LoadNewGameState][EnterState] Entering state ... " );
    }

    public void ExecuteState ( ) {
        Debug.Log ( "[LoadNewGameState][ExecuteState] Executing ... " );

        if ( delayTimer != null ) {
            delayTimer.Timer ( );
            numDelayTicks = delayTimer.timer_tickCount;
        }
                
        if (numDelayTicks > numTicksToDelay && hasGameLoaded == false) {
            Debug.Log ( "[LoadNewGameState][ExecuteState] Starting new round ... " );
            GameRound newRound = new GameRound();
            delayTimer = null;
            hasGameLoaded = true;
        }       
    }

    public event Action<StateBeginExitEvent> StartStateTransition;
}
