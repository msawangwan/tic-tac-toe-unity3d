using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class MainMenuState : IState {
    private Canvas uiCanvas;
    private GameObject mainMenu;

    private Button[] mainMenuButtons;
    private Button btnNewGame;
    private Button btnGameSettings;

    public MainMenuState() {
        uiCanvas = MonoBehaviour.FindObjectOfType<Canvas> ( );
        mainMenu = MonoBehaviour.Instantiate ( Resources.Load ( ResourcePath.mainMenu ) ) as GameObject;
        btnNewGame = mainMenu.GetComponentInChildren<ButtonStartNewGame> ( ).GetComponent<Button>();
        btnNewGame.onClick.AddListener ( HandleOnButtonNewGame );
        MonoBehaviour.print ( "did i get the button? " + btnNewGame.name );
        mainMenu.transform.SetParent ( uiCanvas.transform , false );
    }

    public void EnterState() {
        Debug.Log ( "[MainMenuState][EnterState] Entering new state ... " );
        mainMenu.SetActive ( true );
    }

    public void ExecuteState () {
        Debug.Log ( "[MainMenuState][ExecuteState] Executing State ... " );
    }

    public event Action<StateBeginExitEvent> StartStateTransition;

    private void MapButtons ( ) { // may or may not need to use this
        if ( mainMenu == null ) return;
        else {
            mainMenuButtons = ( Button[] ) mainMenu.GetComponents ( typeof ( Button ) );
        }
    }
    private void HandleOnButtonNewGame() {
        IState nextState = new PlayState();
        IStateTransition transition = new MenuFadeTransition ( mainMenu );
        StateBeginExitEvent exitEvent = new StateBeginExitEvent(nextState, transition);
        StartStateTransition( exitEvent );
    }
}
