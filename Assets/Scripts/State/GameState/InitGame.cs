using UnityEngine;
using System.Collections;

public class InitGame : State {
    private bool hasInitGame = false;

    float elapsed;
    float interval = 1.8f;
    int count = 0;

    protected override void Start ( ) {
        hasInitGame = false;
        base.Start ( );    
    }

    private void Update() {
        elapsed += Time.deltaTime;
        if(elapsed > interval) {
            Debug.Log ( "[TIMER] PING: " + count );
            UpdateState ( );
            elapsed = 0;
            count++;
        }
    }

    public override void UpdateState ( ) {
        if(hasInitGame) {
            Debug.Log ( "[INIT GAME (STATE)] game vars reset ... switching state ... " );
            statemanager.SetMenuGameState ( );
        } else {
            Debug.Log ( "[INIT GAME (STATE)] game vars not set ... resetting !" );
            hasInitGame = ResetGameVars ( );
        }
    }

    private bool ResetGameVars() {
        if(hasInitGame == false) {
            hasInitGame = gamemanager.ResetPlayers ( );
            return true;
        } else {
            return false;
        }
    }
}
