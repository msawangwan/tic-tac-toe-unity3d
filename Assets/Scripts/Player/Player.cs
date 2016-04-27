using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public abstract class Player : MonoBehaviour, IPlayer, IPlayerTurn {
    protected Board gameBoard;

    protected PlayerMoveTable moveTable;
    protected PlayerTurnExitEvent endTurnEvent; // instantiated in child classes

    public int PlayerByID { get; private set; }

    public bool IsTurnActive { get; private set; }
    public bool IsWinner { get; private set; }

    // use as constructor
    public void InitPlayer(Board newGameBoard, int newPlayerByID) {
        gameBoard = newGameBoard;
        PlayerByID = newPlayerByID;

        IsTurnActive = false;
        IsWinner = false;

        moveTable = new PlayerMoveTable();
    }

    public void EnterTurn ( ) {
        IsTurnActive = true;
    }

    public bool TakeTurn ( ) {
        if (IsWinner == false) { // if no round winner yet
            if ( IsTurnActive )
                if ( AttemptMove<Tile> ( ) )
                    return true;
        } else {
            // TODO: Handle Game Over!
        }

        return false;
    }

    public void ExitTurn ( ) {
        IsTurnActive = false;
        HandleOnTurnEnd ( );
    }

    // 'PlayerTurnStateMachine' manages the listeners for this event (to listen, implement 'MadeValidMove')
    public event Action<PlayerTurnExitEvent> ExitTurnEvent;

    protected abstract PlayerTurnExitEvent MadeValidMove ( );
    protected abstract bool AttemptMove<T> ( ) where T : Component;

    protected bool VerifyMove ( Tile selectedTile ) {
        Debug.Log("[Player][VerifyMove] Entering method call.");
        Vector2 selectedTilePosition = selectedTile.ReturnTilePosition();
        if ( gameBoard.TileTable.ContainsKey( selectedTilePosition ) ) {
            if ( selectedTile.isAValidMove == true ) {
                selectedTile.MarkTileAsSelected( PlayerByID );
                moveTable.IncrementMove( selectedTilePosition );
                if ( moveTable.CheckForTicTacToe( ) ) {
                    IsWinner = true;
                }
                return true;
            }
        }
        return false;
    }

    // calling this method, fires event 'ExitTurnEvent'
    private void HandleOnTurnEnd ( ) {
        ExitTurnEvent ( MadeValidMove ( ) );
    }

}
