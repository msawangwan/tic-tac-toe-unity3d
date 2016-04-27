using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;

public class PlayerTurnStateMachine : MonoBehaviour {
    private IPlayer movingPlayer;
    private IPlayer waitingPlayer;

    private IPlayerTurn playerMoveActive;
    private IPlayerTurn playerMoveIdle;
    private IPlayerTurn switchFromIdleToActive {
        set {
            playerMoveActive = value;
            playerMoveActive.ExitTurnEvent += HandlePlayerTurnExitEvent; // add current player as the sole listener
            playerMoveActive.EnterTurn ( );                              // set 'IsTurnActive' to true
        }
    }

    public bool isExecuting { get; private set; }

    public void InitPlayerPlayMachine ( Player startingPlayer ) {
        isExecuting = true;
        waitingPlayer = startingPlayer.GetComponent<IPlayer> ( );
        switchFromIdleToActive = startingPlayer.GetComponent<IPlayerTurn> ( );
        Debug.Log ( "[PlayerPlayMachine][InitPlayerPlayMachine] State Machine Initialised. " );
    }

    public void HandlePlayerTurnExitEvent ( PlayerTurnExitEvent exitTurnEvent ) { // fires event and notifies any listeners
        waitingPlayer = exitTurnEvent.NextPlayer;
        playerMoveIdle = exitTurnEvent.NextPlayerMove;
    }

    private void Update ( ) {
        if ( isExecuting ) {
            Assert.IsFalse ( playerMoveActive == null, "[PlayerPlayMachine][Update] PlayerMachine has no current player!" );
        }

        if ( playerMoveActive.IsTurnActive ) {
            Debug.Log ( "[PlayerPlayMachine][Update] Turn active for: " + playerMoveActive.GetType ( ).ToString ( ) );
            if ( playerMoveActive.TakeTurn ( ) ) {
                playerMoveActive.ExitTurn ( );                               // will set 'IsTurnActive' to false
                playerMoveActive.ExitTurnEvent -= HandlePlayerTurnExitEvent; // remove listener
            }
            return;
        }
        
        movingPlayer = waitingPlayer;
        switchFromIdleToActive = playerMoveIdle;                             // switch the waiting player to the moving player
    }
}
