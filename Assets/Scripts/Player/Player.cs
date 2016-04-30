using UnityEngine;
using System;

//protected PlayerTurnExitEvent endTurnEvent;
public abstract class Player : MonoBehaviour, IPlayer, IPlayerTurn {
    public int PlayerByID { get; private set; }

    public bool IsTurnActive { get; private set; }
    public bool IsWinner { get; private set; }

    protected PlayerMoveTable validMoves { get; private set; }
    protected Grid2D gameBoard { get; private set; } // TODO: find better way to handle this dependency 

    private bool isInGame = false;

    /* Substitute for constructor, call on GameObject instantiantion. */
    public void InitAsNew ( int id ) {
        PlayerByID = id;
        isInGame = false;

        IsTurnActive = false;
        IsWinner = false;
    }

    /* Set board reference on a per round basis */
    public void SetPlayingBoard ( Grid2D board ) {
        gameBoard = board;
    }

    /* Resets player to fresh state for a new round. Initialises
        moves. Allows player to persist between rounds. */
    public void NewGameState ( ) {
        isInGame = true;
        IsWinner = false;

        validMoves = new PlayerMoveTable ( );
    }

    public void RoundOverState () {
        isInGame = false;
        IsTurnActive = false;
    }

    public void EnterTurn ( ) {
        IsTurnActive = true;
    }

    /* Called in update while IsTurnActive is true. */
    public bool TakeTurn ( ) {
        if (IsWinner == false) { // game is live branch
            if ( IsTurnActive )
                if ( AttemptMove<Tile> ( ) )
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
    protected bool VerifyMove ( Tile selectedTile ) {
        Vector2 selectedTilePosition = selectedTile.Position;
        if ( gameBoard.grid2D.VertexTable.ContainsKey( selectedTilePosition ) ) {
            if ( selectedTile.IsAValidMove == true ) {
                selectedTile.MarkTileAsSelected( PlayerByID );
                validMoves.IncrementMove( selectedTilePosition );
                if ( validMoves.CheckForTicTacToe( ) ) {
                    IsWinner = true;
                }
                return true;
            }
        }
        return false;
    }

    /* Fires an 'ExitTurnEvent' on end of turn. */
    private void OnTurnEnd ( ) {
        RaiseTurnCompletedEvent ( MadeValidMove ( ) );
    }

}
