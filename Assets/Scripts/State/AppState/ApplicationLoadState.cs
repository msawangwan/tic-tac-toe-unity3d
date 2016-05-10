using UnityEngine;
using UnityEngine.Assertions;
using System;
using System.Collections;

public class ApplicationLoadState :  IState {
    private float loadTicks = 0;
    private float currentStopwatchTick = 0;
    private Ticker stopwatch;

    private bool isInitialised = false;

    public bool IsStateExecuting { get; private set; }

    public ApplicationLoadState() {
        IsStateExecuting = true;
        loadTicks = 1.5f;
        float interval = .5f;
        stopwatch = new Ticker ( interval );
    }

    public void EnterState ( ) { }

    public void ExecuteState() {
        if ( isInitialised == false ) {
            stopwatch.Timer ( ); // do a little counting
            currentStopwatchTick = stopwatch.TickCount;
            if ( currentStopwatchTick > loadTicks ) {
                isInitialised = true;
                IsStateExecuting = false; ;
                OnApplicationLoadComplete ( );
            }
        }   
    }

    public event Action<StateBeginExitEvent> RaiseStateChangeEvent;

    private void OnApplicationLoadComplete() {
        IState nextState = new MainMenuState();
        IStateTransition transition = new LoadingTransition();
        StateBeginExitEvent exitEvent = new StateBeginExitEvent(nextState, transition);

        if ( RaiseStateChangeEvent != null )
            RaiseStateChangeEvent ( exitEvent );    
    }
}