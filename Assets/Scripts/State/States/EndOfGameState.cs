using UnityEngine;
using System;
using System.Collections;

public class EndOfGameState : IState {
    private UserInterfaceMenu menu;

    public bool IsStateExecuting { get; private set; }

    public EndOfGameState() {
        IsStateExecuting = true;
        menu = new EndOfRoundMenu ( );
        menu.buttonEvent.RaiseUIEvent += OnUIButtonEvent;
    }

    public void EnterState ( ) {
        menu.MakeActiveInScene ( );
    }
    
    public void ExecuteState ( ) {
        if ( IsStateExecuting ) {

        }
    }

    public event Action<StateBeginExitEvent> RaiseStateChangeEvent;

    // signature of a UI reference defined event -- called when 'buttonEvent' action fires, which then calls 'IState" event 'RaiseStateChangeEvent'
    private void OnUIButtonEvent( StateBeginExitEvent nextStateParameters ) {
        IsStateExecuting = false;

        if ( RaiseStateChangeEvent != null )
            RaiseStateChangeEvent ( nextStateParameters );

        menu.buttonEvent.RaiseUIEvent -= OnUIButtonEvent;
    }
}
