using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class RoundLoadState : IState {
    public bool IsStateExecuting { get; private set; }

    private Ticker loadingCountdown;
    private GameRound round;
    private IEnumerable transitionAsset;

    private float loadDelay;
    private float numDelayTicks = 0;

    private bool hasGameLoaded = false;

    /* Constructor. */
    public RoundLoadState( float loadTime ) {
        IsStateExecuting = true;
        hasGameLoaded = false;

        loadingCountdown = new Ticker ( .6f );
        loadDelay = loadTime;
    }
    
    public void EnterState ( ) { }

    public void ExecuteState ( ) {
        if ( hasGameLoaded == true ) {            // Exit State if loading is done
            OnGameSetupCompleted ( );
        }

        if ( loadingCountdown != null ) {         // Poll load timer
            PollLoadTimer ( );
        }
                
        if (numDelayTicks > loadDelay && hasGameLoaded == false) {  // Load round assets after loadtime delay, state has RTC
            LoadRoundAssets ( );

            loadingCountdown = null;
            hasGameLoaded = true;
        }
    }

    public event Action<StateBeginExitEvent> RaiseStateChangeEvent;

    private void PollLoadTimer() {
        loadingCountdown.Timer ( );
        numDelayTicks = loadingCountdown.TickCount;
    }

    private void LoadRoundAssets ( ) {
        round = new GameRound ( );
        round.LoadNewGrid ( );
        transitionAsset = round.LoadedTransitionIntroAsset;
        LoadPlayersIntoRound ( );
    }

    private void LoadPlayersIntoRound() {
        round.AddPlayerControlType ( false ); // AI currently is always 0
        round.AddPlayerControlType ( true );
    }

    private void OnGameSetupCompleted ( ) {
        IsStateExecuting = false;

        IState nextState = new RoundState( round );
        IStateTransition transition = new LoadingTransition( transitionAsset );
        StateBeginExitEvent exitEvent = new StateBeginExitEvent(nextState, transition);

        if ( RaiseStateChangeEvent != null )
            RaiseStateChangeEvent ( exitEvent );
    }
}
