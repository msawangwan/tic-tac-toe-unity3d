using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;

public class PlayerTurnStateMachine : MonoBehaviour {
    private IRound currentRound;

    private IPlayer movingPlayer;
    private IPlayer idlePlayer;

    private IPlayerTurn turnCurrent;
    private IPlayerTurn turnNext;
    private IPlayerTurn setTurnCurrent {
        set {
            turnCurrent = value;
            turnCurrent.ExitTurnEvent += HandlePlayerTurnExitEvent; // add current player as the sole listener
            turnCurrent.EnterTurn ( );                              // set 'IsTurnActive' to true
        }
    }

    public bool isExecuting { get; private set; }

    // use as constructor
    public void InitPlayerPlayMachine ( IRound newRound, Player startingPlayer ) {
        currentRound = newRound;
        movingPlayer = startingPlayer.GetComponent<IPlayer> ( );
        setTurnCurrent = startingPlayer.GetComponent<IPlayerTurn> ( );
    }

    // called by 'GameRound'
    public void StartNewRound() {
        isExecuting = true;
    }

    public void HandlePlayerTurnExitEvent ( PlayerTurnExitEvent exitTurnEvent ) { // fires event and notifies any listeners
        idlePlayer = exitTurnEvent.NextPlayer;
        turnNext = exitTurnEvent.NextPlayerMove;
    }

    // TODO: better way to check for end of round?
    private void Update ( ) {
        CheckForEndOfRound ( );

        if ( isExecuting ) {
            Assert.IsFalse ( turnCurrent == null, "[PlayerTurnStateMachine][Update] PlayerMachine has no current player!" );

            if ( turnCurrent.IsTurnActive ) {
                Debug.Log ( "[PlayerTurnStateMachine][Update] Turn active for: " + turnCurrent.GetType ( ).ToString ( ) );
                if ( turnCurrent.TakeTurn ( ) ) {
                    turnCurrent.ExitTurn ( );                               // will set 'IsTurnActive' to false
                    turnCurrent.ExitTurnEvent -= HandlePlayerTurnExitEvent; // remove listener
                    if ( movingPlayer.IsWinner ) {
                        isExecuting = false;
                        CheckForEndOfRound ( );
                    }
                }
                return;
            }

            if ( movingPlayer.IsWinner ) {
                Debug.Log ( "[PlayerTurnStateMachine][Update] Game over, winner is: " + turnCurrent.GetType ( ).ToString ( ) );
                isExecuting = false;
            }

            CheckForEndOfRound ( );

            movingPlayer = idlePlayer;
            setTurnCurrent = turnNext;    // switch the waiting player to the moving player
        }

        CheckForEndOfRound ( );
    }

    private void CheckForEndOfRound() {
        if ( isExecuting == false && (movingPlayer.IsWinner == true || idlePlayer.IsWinner == true ) ) {
            Debug.Log ( "[PlayerTurnStateMachine][CheckForEndOfRound] Gameover, deleting self from scene ... " );
            currentRound.EndCurrentRound ( );
            Destroy ( gameObject );
        }
    }
}
