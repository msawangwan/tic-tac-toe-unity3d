using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;

public class PlayerPlayMachine : MonoBehaviour {
    private IPlayer player;
    private IPlayer nextPlayer;
    private IPlayer currentPlayer {
        set {
            player = value;
            player.ExitTurnEvent += HandlePlayerTurnExitEvent; // add listener
            player.EnterTurn ( );
        }
    }

    public bool isExecuting { get; private set; }

    public void InitPlayerPlayMachine(IPlayer startingPlayer) {
        isExecuting = true;
        currentPlayer = startingPlayer;
        Debug.Log ( "[PlayerPlayMachine][InitPlayerPlayMachine] State Machine Initialised. " );
    }

    // fires event and notifies any listeners
    public void HandlePlayerTurnExitEvent( PlayerTurnExitEvent exitTurnEvent ) {
        nextPlayer = exitTurnEvent.NextPlayer;
    }

    private void Update() {
        if ( isExecuting ) {
            Assert.IsFalse ( player == null , "[PlayerPlayMachine][Update] PlayerMachine has no current player!" );
        }

        if (player.IsTurnActive) {
            Debug.Log ( "[PlayerPlayMachine][Update] Turn active for: " + player.GetType().ToString() );
            player.TakeTurn ( );
            return;
        }
    }
}
