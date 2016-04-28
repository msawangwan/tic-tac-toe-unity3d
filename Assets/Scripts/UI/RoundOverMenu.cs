using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using System;
using System.Collections;
/// <summary>
/// TODO: Make this an abstract base class for all menu gameobjects
/// </summary>
public class RoundOverMenu : IUI {
    private GameObject menuObject;

    private Canvas uiCanvas;

    private Button btnPlayAgain;
    private Button btnReturnToMainMenu;
    private Button btnSettings;
    private Button[] menuButtons;

    public event Action<StateBeginExitEvent> RaiseButtonEvent;

    public IUI menuReference { get; private set; }

    public RoundOverMenu() {
        uiCanvas = MonoBehaviour.FindObjectOfType<Canvas> ( );

        menuObject = MonoBehaviour.Instantiate<GameObject> ( Resources.Load<GameObject> ( ResourcePath.roundOverMenu ) );
        menuObject.SetActive ( false );
        menuObject.transform.SetParent ( uiCanvas.transform, false );

        MapButtons ( );
    }

    public void MakeActiveInScene() {
        if ( menuObject )
            menuObject.SetActive ( true );
    }

    private void MapButtons ( ) {
        if ( menuObject == null ) {
            Assert.IsFalse ( menuObject == null , "[RoundOverMenu][MapButtons] No menu object set" );
        } else {
            menuButtons = menuObject.GetComponentsInChildren<Button> ( true );
            foreach ( Button btn in menuButtons ) {
                if (btn.CompareTag( TagsUI.startNewGameBtn )) {              // btn - starts a new round
                    btn.onClick.RemoveAllListeners ( );
                    btn.onClick.AddListener ( ( ) => {
                        float loadingTime = 1.8f;
                        IState nextState = new LoadNewGameState( loadingTime );
                        IStateTransition transition = new MenuExitTransition ( menuObject );
                        StateBeginExitEvent exitEvent = new StateBeginExitEvent( nextState, transition );
                        RaiseButtonEvent ( exitEvent );                      // notify listeners of the event
                    } );
                } else if ( btn.CompareTag( TagsUI.returnToMainMenuBtn ) ) { // btn - returns to main menu
                    btn.onClick.RemoveAllListeners ( );
                    btn.onClick.AddListener ( ( ) => {
                        IState nextState = new MainMenuState();
                        IStateTransition transition = new LoadingTransition( menuObject );
                        StateBeginExitEvent exitEvent = new StateBeginExitEvent( nextState, transition );
                        RaiseButtonEvent ( exitEvent );                     // notify event subscribers
                    } );
                } else if ( btn.CompareTag( TagsUI.settingsMenuBtn ) ) {    // btn - opens settings menu
                    btn.onClick.RemoveAllListeners ( );
                    btn.onClick.AddListener ( ( ) => {
                        // TODO: implement settings menu
                        Debug.Log ( "[RoundOverMenu][OnToggleSettingsMenu] Settings menu not yet implemented ... " );
                    } );
                }
            }
        }
    }
}
