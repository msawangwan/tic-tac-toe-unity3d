using System;
using UnityEngine;
using UnityEngine.UI;

public class EndOfRoundMenu : UserInterfaceMenu, IUIEvent, ITextOutput {
    private string winningPlayer = "";
    private MusicMasterController musicplayer;

    /* Constructor. */
    public EndOfRoundMenu( string winningPlayerByName ) : base() {
        buttonEvent = this;
        musicplayer = MonoBehaviour.FindObjectOfType<MusicMasterController> ( );
        menuObject = MonoBehaviour.Instantiate<GameObject> ( Resources.Load<GameObject> ( ResourcePath.roundOverMenu ) );
        menuObject.SetActive ( false );
        menuObject.transform.SetParent ( uiCanvasReference.transform, false );

        winningPlayer = winningPlayerByName + winningPlayer;

        FindButtonsInChildren ( );
        MapText ( );
    }

    public void MapText ( ) {
        Text[] txts = menuObject.GetComponentsInChildren<Text>( true );
        foreach ( Text txt in txts ) {
            if (txt.CompareTag(TagsUI.menuSubHeader)) {
                txt.text = winningPlayer;
                break;
            }
        }
    }

    public event Action<StateBeginExitEvent> RaiseUIEvent;

    protected override void MapButtons ( ) {
        foreach ( Button btn in menuButtons ) {
            if ( btn.CompareTag ( TagsUI.startNewGameBtn ) ) {            // btn - starts a new round
                btn.onClick.RemoveAllListeners ( );
                btn.onClick.AddListener ( ( ) => {
                    float fadeTime = 1.8f;
                    SFXMasterController.PlayNoMercyClip ( );
                    IState nextState = new RoundLoadState ( fadeTime );
                    IStateTransition transition = new MenuExitTransition ( menuObject );
                    StateBeginExitEvent newRoundState = new StateBeginExitEvent ( nextState, transition );
                    audioplayer.PlayOneShot ( btnClick );
                    musicplayer.MusicCheck ( true );

                    RaiseUIEvent ( newRoundState );
                } );
            } else if ( btn.CompareTag ( TagsUI.returnToMainMenuBtn ) ) { // btn - returns to main menu
                btn.onClick.RemoveAllListeners ( );
                btn.onClick.AddListener ( ( ) => {
                    IState nextState = new MainMenuState ();
                    IStateTransition transition = new LoadingTransition( menuObject );
                    StateBeginExitEvent returnToMainMenustate = new StateBeginExitEvent ( nextState, transition );
                    audioplayer.PlayOneShot ( btnClick );

                    RaiseUIEvent ( returnToMainMenustate );
                } );
            } else if ( btn.CompareTag ( TagsUI.settingsMenuBtn ) ) {     // btn - opens settings menu
                btn.onClick.RemoveAllListeners ( );
                btn.onClick.AddListener ( ( ) => {
                    audioplayer.PlayOneShot ( btnClick );
                    Debug.Log ( "[EndOfRoundMenu][OnToggleSettingsMenu] Settings menu not yet implemented ... " );
                } );
            }
        }
    }

}
