using UnityEngine;
using UnityEngine.Assertions;
using System;
using System.Collections;

public class LoadApplicationState :  IState {
    private float loadTicks = 0;
    private float currentStopwatchTick = 0;
    private Utility stopwatch;

    private bool isInitialised = false;

    public bool IsStateExecuting { get; private set; }
    public bool IsStateExit { get; private set; }

    public LoadApplicationState() {
        IsStateExecuting = true;
        loadTicks = 1.5f;
        float interval = 1.2f;
        stopwatch = new Utility ( interval );
    }

    public void EnterState ( ) {
        Debug.Log ( "[LoadApplicationState][EnterState] Entering state ... " );
    }

    public void ExecuteState() {
        if ( isInitialised == false ) {
            stopwatch.Timer ( ); // do a little counting
            currentStopwatchTick = stopwatch.timer_tickCount;
            if ( currentStopwatchTick > loadTicks ) {
                isInitialised = true;
                IsStateExecuting = false; ;
                HandleOnApplicationLoadComplete ( );
            }
        }   
    }

    public event Action<StateBeginExitEvent> StartStateTransition;

    private void HandleOnApplicationLoadComplete() {
        Debug.Log ( "[LoadApplicationState][HandleOnApplicationLoadComplete] Exiting state." );
        IState nextState = new MainMenuState();
        IStateTransition transition = new ExitLoadingTransition();
        StateBeginExitEvent exitEvent = new StateBeginExitEvent(nextState, transition);
        StartStateTransition ( exitEvent );
    }
}