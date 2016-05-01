using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;

public class PlayerTurnSystem : MonoBehaviour {
    private IRound currentRound;

    private IPlayer movingPlayer;
    private IPlayer idlePlayer;

    private IPlayerTurn turnCurrent;
    private IPlayerTurn turnNext;

    /* Everytime the movingPlayer variable is updated, the value is
        piped through this setter to add a listener to the new value. */
    private IPlayerTurn setTurnCurrent {
        set {
            turnCurrent = value;
            turnCurrent.RaiseTurnCompletedEvent += HandlePlayerTurnExitEvent; // add current player as the sole listener
            turnCurrent.EnterTurn ( );                                        // set 'IsTurnActive' to true
        }
    }

    public bool IsExecuting { get; private set; }

    /* Call this method before starting the first turn, as this initializes the necessarry
        local variables. */
    public void SetStartingPlayer ( IRound newRound, Player startingPlayer ) {
        currentRound = newRound;
        movingPlayer = startingPlayer.GetComponent<IPlayer> ( );
        setTurnCurrent = startingPlayer.GetComponent<IPlayerTurn> ( );
    }

    /* No turns will be processed until this method call. */
    public void StartFirstTurn() {
        IsExecuting = true;
    }

    /* Event handler, fires on end of any players turn. */
    public void HandlePlayerTurnExitEvent ( PlayerTurnExitEvent exitTurnEvent ) { // fires event and notifies any listeners
        idlePlayer = exitTurnEvent.NextPlayer;
        turnNext = exitTurnEvent.NextPlayerMove;
    }

    /* Loop until IsExecuting is False. */
    private void Update ( ) {
        if ( IsExecuting ) {
            CheckForEndOfRound ( );
            Debug.Log ( movingPlayer.GetType ( ).ToString ( ) );
            if ( turnCurrent.IsTurnActive ) {
                if ( turnCurrent.TakeTurn ( ) ) {
                    turnCurrent.ExitTurn ( );                                         // will set 'IsTurnActive' to false
                    turnCurrent.RaiseTurnCompletedEvent -= HandlePlayerTurnExitEvent; // remove listener
                    if ( movingPlayer.IsWinner ) {
                        IsExecuting = false;
                        CheckForEndOfRound ( );
                    }
                }
                return;
            }

            if ( movingPlayer.IsWinner ) {
                IsExecuting = false;
            }

            CheckForEndOfRound ( );

            movingPlayer = idlePlayer;
            setTurnCurrent = turnNext;    // switch the waiting player to the moving player

            CheckForEndOfRound ( );
        }
    }

    /* Win condition check. */
    private void CheckForEndOfRound() {
        if ( IsExecuting == false && (movingPlayer.IsWinner == true || idlePlayer.IsWinner == true ) ) {
            currentRound.EndCurrentRound ( );
            Destroy ( gameObject );
        }
    }
}
