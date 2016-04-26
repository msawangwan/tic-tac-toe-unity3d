using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PlayerComputer : Player, IPlayer {
    public bool IsTurnActive { get; private set; }

    protected override bool AttemptMove<T>() {
        foreach ( Vector2 move in gameboard.TileCoordinates ) {
            if ( gameboard.TileTable.ContainsKey ( move ) ) {
                Tile selectedTile = gameboard.TileTable[move];
                if ( selectedTile.isAValidMove == true ) {
                    IsTurnActive = turn.ExecuteTurn ( selectedTile );
                    break;
                } else {
                    continue;
                }
            }
        }
    }

    public void EnterTurn ( ) {
        IsTurnActive = true;
    }

    public void TakeTurn ( ) {
        AttemptMove<Tile> ( );
    }

    public void ExitTurn ( ) {
        IsTurnActive = false;
    }

    public event Action<PlayerTurnExitEvent> ExitTurnEvent;

    private void OnTurnEnd ( ) {
        Logger.DebugToConsole ( "PlayerHuman" , "OnTurnEnd" , "Ending turn." );
        IPlayer nextPlayer = FindObjectOfType<PlayerHuman>();
        PlayerTurnExitEvent endTurnEvent = new PlayerTurnExitEvent( nextPlayer );
        ExitTurnEvent ( endTurnEvent );
    }

}
