using UnityEngine;
using UnityEngine.UI;
using System;

public class MainMenuState : IState {
    private UserInterfaceMenu menu;

    public bool IsStateExecuting { get; private set; }

    public MainMenuState() {
        IsStateExecuting = true;
        menu = new MainMenu ( );
        menu.buttonEvent.RaiseUIEvent += OnUIButtonEvent;
    }

    public void EnterState() {
        menu.MakeActiveInScene ( );
    }

    public void ExecuteState () {
        if ( IsStateExecuting ) {

        }
    }

    public event Action<StateBeginExitEvent> RaiseStateChangeEvent;

    public void OnUIButtonEvent( StateBeginExitEvent nextStateParameters ) {
        IsStateExecuting = false;

        if (RaiseStateChangeEvent != null)
            RaiseStateChangeEvent ( nextStateParameters );

        menu.buttonEvent.RaiseUIEvent -= OnUIButtonEvent;
    }
}