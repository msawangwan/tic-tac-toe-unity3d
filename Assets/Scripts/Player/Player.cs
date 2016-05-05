using UnityEngine;
using System;

public abstract class Player : MonoBehaviour, IPlayer, IPlayerTurn {
    public int PlayerByID { get; private set; }

    public string PlayerName { get; private set; }
    public bool IsTurnActive { get; private set; }
    public bool IsWinner { get; private set; }

    protected PlayerMoveTable validMoves { get; private set; }

    /* Substitute for constructor, call on GameObject instantiantion. */
    public virtual void InitAsNew ( int id, string playerName ) {
        PlayerByID = id;
        PlayerName = playerName;

        IsTurnActive = false;
        IsWinner = false;
    }

    /* Call on start of each round! Resets player to fresh state for 
        a new round. Initialises moves. Allows player to persist between rounds. */
    public virtual void NewGameState ( ) {
        IsWinner = false;

        validMoves = new PlayerMoveTable ( );
    }

    /* Currently Not Being Called!!! */
    public void RoundOverState () {
        IsTurnActive = false;
        Debug.Log ( "DESTROYED PLAYER: " + gameObject.name );
        Destroy ( gameObject );
    }

    public void EnterTurn ( ) {
        IsTurnActive = true;
    }

    /* Called in update while IsTurnActive is true. */
    public bool TakeTurn ( ) {
        if (IsWinner == false) { // game is live branch
            if ( IsTurnActive )
                if ( AttemptMove<Grid2DInteractable> ( ) )
                    return true;
        } else {                 // game is over branch
            RoundOverState ( );
        }
        return false;
    }

    public void ExitTurn ( ) {
        IsTurnActive = false;
        OnTurnEnd ( );
    }

    /* Notifies listeners of the turn-based system when player ends their turn. */
    public event Action<PlayerTurnExitEvent> RaiseTurnCompletedEvent;

    /* Child class defined methods. MadeValidMove is an event that signifies player has
        successfully completed their turn and AttemptMove defines how the player moves. */
    protected abstract PlayerTurnExitEvent MadeValidMove ( );
    protected abstract bool AttemptMove<T> ( ) where T : Component;

    /* When Player selects a Vector2 represented as a tile on the gameboard, this method checks 
        to see if the selection is a valid move against a table of precomputed Vector2s. */
    protected bool VerifyMove ( Transform vertex2D, Color player ) {
        if ( vertex2D.GetComponent<Grid2DInteractable>( ) ) {
            vertex2D.GetComponent<Grid2DInteractable> ( ).SetOwner ( PlayerByID );
            vertex2D.GetComponent<Grid2DTile> ( ).MarkByPlayerColor ( player );

            validMoves.IncrementMove ( vertex2D.transform.position );
            if ( validMoves.CheckForTicTacToe ( ) ) {
                IsWinner = true;
            }
            return true;
        }
        return false;
    }

    /* Fires an 'ExitTurnEvent' on end of turn. */
    private void OnTurnEnd ( ) {
        RaiseTurnCompletedEvent ( MadeValidMove ( ) );
    }
}