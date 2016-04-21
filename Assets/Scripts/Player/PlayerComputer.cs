using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerComputer : Player {
    protected override void Update() {
        base.Update( );
    }

    protected override void AttemptMove<T>() {
        foreach ( Vector2 move in gameboard.TileCoordinates ) {
            if ( gameboard.TileTable.ContainsKey ( move ) ) {
                Tile selectedTile = gameboard.TileTable[move];
                if ( selectedTile.isAValidMove == true ) {
                    hasMadeMove = turn.ExecuteTurn ( selectedTile );
                    break;
                } else {
                    continue;
                }
            }
        }
    }
}
