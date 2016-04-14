using UnityEngine;
using System.Collections;

public class StartGame : GameState {
    private bool hasGameStarted = false;

    protected override void Start ( ) {
        base.Start( );
    }

    public void EnterStartGameState ( ) {
        if(hasGameStarted == false) {
            gamemanager.StartGameManager( );
        }
    }
}
