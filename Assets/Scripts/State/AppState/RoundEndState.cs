using UnityEngine;
using System;
using System.Collections;

public class RoundEndState : IState {
    private UserInterfaceMenu menu;

    public bool IsStateExecuting { get; private set; }

    public RoundEndState( GameRound round ) {
        IsStateExecuting = true;
        menu = new EndOfRoundMenu ( round.Game.GameWinner );
        menu.buttonEvent.RaiseUIEvent += OnUIButtonEvent;
    }

    public void EnterState ( ) {
        menu.MakeActiveInScene ( );       
    }
    
    public void ExecuteState ( ) { if ( IsStateExecuting ) { } return; }

    public event Action<StateBeginExitEvent> RaiseStateChangeEvent;

    /* Handler method for when a UI button is selected. */
    private void OnUIButtonEvent( StateBeginExitEvent nextStateParameters ) {
        IsStateExecuting = false;

        if ( RaiseStateChangeEvent != null )
            RaiseStateChangeEvent ( nextStateParameters );

        menu.buttonEvent.RaiseUIEvent -= OnUIButtonEvent;
    }
}
