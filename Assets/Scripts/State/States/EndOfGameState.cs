using UnityEngine;
using System;
using System.Collections;

public class EndOfGameState : IState {
    private GameObject uiMenuObject;

    private RoundOverMenu menuInstance;
    private StateBeginExitEvent exitEventParams;

    public bool IsStateExecuting { get; private set; }

    public EndOfGameState() {
        IsStateExecuting = true;
        menuInstance = new RoundOverMenu ( );
        menuInstance.RaiseButtonEvent += HandleOnUIButtonEvent;
    }

    public void EnterState ( ) {
        menuInstance.MakeActiveInScene ( );
    }

    // TODO: any thing to run?
    public void ExecuteState ( ) {}

    public event Action<StateBeginExitEvent> RaiseStateChangeEvent;

    private void OnStateChangeEvent ( ) {
       RaiseStateChangeEvent ( exitEventParams );
    }

    // signature of a UI reference defined event
    private void HandleOnUIButtonEvent( StateBeginExitEvent nextStateParameters ) {
        IsStateExecuting = false;
        exitEventParams = nextStateParameters;
        OnStateChangeEvent ( ); // fire IState implemented event, when UI event fires his event
    }
}
