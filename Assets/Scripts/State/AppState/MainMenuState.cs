using UnityEngine;
using UnityEngine.UI;
using System;

public class MainMenuState : IState {
    private UserInterfaceMenu menu;

    private MusicMasterController musicplayer;

    public bool IsStateExecuting { get; private set; }

    public MainMenuState() {
        IsStateExecuting = true;
        menu = new MainMenu ( );
        menu.buttonEvent.RaiseUIEvent += OnUIButtonEvent;

        musicplayer = MonoBehaviour.FindObjectOfType<MusicMasterController> ( );
    }

    public void EnterState() {
        menu.MakeActiveInScene ( );
        musicplayer.MusicCheck ( false );
    }

    public void ExecuteState () {
        if ( IsStateExecuting ) {

        }
    }

    public event Action<StateBeginExitEvent> RaiseStateChangeEvent;

    public void OnUIButtonEvent( StateBeginExitEvent nextStateParameters ) {
        IsStateExecuting = false;

        musicplayer.MusicCheck ( true );

        if (RaiseStateChangeEvent != null)
            RaiseStateChangeEvent ( nextStateParameters );

        menu.buttonEvent.RaiseUIEvent -= OnUIButtonEvent;
    }
}