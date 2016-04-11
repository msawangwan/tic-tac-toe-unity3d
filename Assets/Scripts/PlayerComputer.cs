using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerComputer : Player {
    protected override void Awake( ) {
        base.Awake( );
    }

    protected override void Update() {
        base.Update( );
    }

    protected override void MakeAMove<T>() {
        foreach(Vector2 move in gameboard.TileCoordinates) {
            if (gameboard.MoveTable.ContainsKey( move )) {
                if(gameboard.MoveTable[move] == true) {
                    Tile selectedTile = gameboard.TileTable[move];
                    gamemanager.MakeMove( selectedTile, playerID );
                    break;
                } else {
                    continue;
                }
            }
        }
    }
}
