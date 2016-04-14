using UnityEngine;
using System.Collections;

public class StartGame : State {
    private bool hasGameStarted = false;

    protected override void Start ( ) {
        base.Start ( );
    }

    public override void UpdateState ( ) {

    }

    public void EnterStartGameState ( ) {
        if(hasGameStarted == false) {
            gamemanager.StartGameManager( );
        }
    }
}
