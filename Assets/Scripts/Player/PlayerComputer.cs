using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PlayerComputer : Player, IPlayerMove {
    public bool HasMadeValidMove { get; private set; }

    private Grid2D gameboard;

    public void InitAi() {
        gameboard = FindObjectOfType<Grid2D> ( );
    }

    protected override bool AttemptMove<T>() {
        HasMadeValidMove = false;
        foreach ( Transform v in gameboard.Grid2DData.GridObject.transform ) {         
            if ( v.GetComponent<GridInteractableObject>( ).IsUnMarked( ) ) {
                HasMadeValidMove = VerifyMove ( v.transform, Color.red );
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
