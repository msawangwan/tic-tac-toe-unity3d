using UnityEngine;
using System.Collections;

public class StateManager : MonoBehaviour {
    private InitGame initalgamestate;
    private MenuGame menugamestate;
    private PauseGame pausegamestate;
    private StartGame startgamestate;
    private PlayGame playgamestate;
    private EndGame endgamestate;

    private State currentGameState;
    private State nullState = null;

    public void SetGameState ( State newGameState ) {
        currentGameState = newGameState;
    }

    public InitGame SetInitialGameState() {
        if(initalgamestate != null) {
            return initalgamestate;
        } else {
            return (InitGame) nullState;
        }
    }

    public MenuGame SetMenuGameState() {
        if ( menugamestate != null ) {
            return menugamestate;
        } else {
            return ( MenuGame ) nullState;
        }
    }

    public StartGame SetStartGameState() {
        if ( startgamestate != null ) {
            return startgamestate;
        } else {
            return ( StartGame ) nullState;
        }
    }

    public PlayGame SetPlayGameState ( ) {
        if ( playgamestate != null ) {
            return playgamestate;
        } else {
            return ( PlayGame ) nullState;
        }
    }

    public EndGame SetEndGameState ( ) {
        if ( endgamestate != null ) {
            return endgamestate;
        } else {
            return ( EndGame ) nullState;
        }
    }

    public void StartStateManager() {
        GetReferences ( );
    }

    private void GetReferences ( ) {
        initalgamestate = GetComponentInChildren<InitGame> ( ) as InitGame;
        menugamestate = GetComponentInChildren<MenuGame> ( ) as MenuGame;
        pausegamestate = GetComponentInChildren<PauseGame> ( ) as PauseGame;
        startgamestate = GetComponentInChildren<StartGame> ( ) as StartGame;
        playgamestate = GetComponentInChildren<PlayGame> ( ) as PlayGame;
        endgamestate = GetComponentInChildren<EndGame> ( ) as EndGame;
    }

}
