using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class LoadRoundState : IState {
    private GameRound round;
    private IEnumerable transitionAsset;

    private Ticker delay;

    private float loadDelay;
    private float numDelayTicks = 0;

    private bool hasGameLoaded = false;

    public bool IsStateExecuting { get; private set; }

    public LoadRoundState( float loadTime ) {
        IsStateExecuting = true;
        hasGameLoaded = false;

        delay = new Ticker ( .6f );
        loadDelay = loadTime;
    }

    public void EnterState ( ) { }

    public void ExecuteState ( ) {
        if ( hasGameLoaded == true ) { // Exit State if loading is done
            OnGameSetupCompleted ( );
        }

        if ( delay != null ) {         // Poll load timer
            PollLoadTimer ( );
        }
                
        if (numDelayTicks > loadDelay && hasGameLoaded == false) {  // Load round assets after loadtime delay, state has RTC
            LoadRoundAssets ( );

            delay = null;
            hasGameLoaded = true;
        }
    }

    public event Action<StateBeginExitEvent> RaiseStateChangeEvent;

    private void PollLoadTimer() {
        delay.Timer ( );
        numDelayTicks = delay.TickCount;
    }

    private void LoadRoundAssets ( ) {
        round = new GameRound ( );
        round.AddPlayerControlType ( true );
        round.AddPlayerControlType ( false );
        round.LoadNewGrid ( );
        transitionAsset = round.LoadedTransitionAsset;
    }

    private void OnGameSetupCompleted ( ) {
        IsStateExecuting = false;

        IState nextState = new RoundState( round );
        IStateTransition transition = new LoadingTransition( transitionAsset );
        StateBeginExitEvent exitEvent = new StateBeginExitEvent(nextState, transition);

        if ( RaiseStateChangeEvent != null ) {
            RaiseStateChangeEvent ( exitEvent );
        }
    }
}
