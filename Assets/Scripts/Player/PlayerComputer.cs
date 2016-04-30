using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PlayerComputer : Player, IPlayerMove {
    public bool HasMadeValidMove { get; private set; }

    // refactor, use more intelligent implementation over 'foreach'
    protected override bool AttemptMove<T>() {
        HasMadeValidMove = false;
        foreach ( Vector2 move in gameBoard.grid2D.VertexTable.Keys ) {
            Tile selectedTile = gameBoard.grid2D.VertexTable[move] as Tile;
            if ( selectedTile.IsAValidMove == true ) {
                HasMadeValidMove = VerifyMove ( selectedTile );
                break;
            }
        }
        return HasMadeValidMove;
    }

    // base class needs an instance of 'endTurnEvent'
    protected override PlayerTurnExitEvent MadeValidMove ( ) {
        Logger.DebugToConsole ( "PlayerComputer", "MadeValidMove", "Ending turn." );
        Player opponentPlayer = FindObjectOfType<PlayerHuman>();
        IPlayer nextPlayer = opponentPlayer.GetComponent<IPlayer>();
        IPlayerTurn nextPlayerTurn = opponentPlayer.GetComponent<IPlayerTurn>();

        return new PlayerTurnExitEvent ( nextPlayer, nextPlayerTurn );
    }
}
