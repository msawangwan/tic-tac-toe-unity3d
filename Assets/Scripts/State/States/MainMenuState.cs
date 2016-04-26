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

    public bool IsStateExecuting { get; private set; }
    public bool IsStateExit { get; private set; }

    public MainMenuState() {
        IsStateExecuting = true;
        uiCanvas = MonoBehaviour.FindObjectOfType<Canvas> ( );
        mainMenu = MonoBehaviour.Instantiate ( Resources.Load ( ResourcePath.mainMenu ) ) as GameObject;
        mainMenu.transform.SetParent ( uiCanvas.transform , false );
        MapButtons ( );
    }

    public void EnterState() {
        Debug.Log ( "[MainMenuState][EnterState] Entering state ... " );
        mainMenu.SetActive ( true );
    }

    public void ExecuteState () {
        Debug.Log ( "[MainMenuState][ExecuteState] Executing state ... " );
    }

    public event Action<StateBeginExitEvent> StartStateTransition;

    private void MapButtons ( ) {
        if ( mainMenu == null ) return;
        else {
            mainMenuButtons = mainMenu.GetComponentsInChildren<Button> ( true );
            foreach ( Button btn in mainMenuButtons ) {
                if (btn.CompareTag(TagsUI.startNewGameBtn)) { // btn - start a new game
                    btnNewGame = btn.GetComponent<Button> ( );
                    btnNewGame.onClick.RemoveAllListeners ( );
                    btnNewGame.onClick.AddListener ( HandleOnButtonNewGame );
                }
            }
        }
    }

    private void HandleOnButtonNewGame() {
        float loadingTime = 1.8f;
        IState nextState = new LoadNewGameState( loadingTime );
        IStateTransition transition = new MenuFadeTransition ( mainMenu );
        StateBeginExitEvent exitEvent = new StateBeginExitEvent( nextState, transition );
        StartStateTransition( exitEvent );
    }
}