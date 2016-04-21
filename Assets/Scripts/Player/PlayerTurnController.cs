using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerTurnController {
    private Board board;

    private Player player;
    private PlayerID playerByID;
    
    public bool isRoundOver { get; private set; }

    public PlayerTurnController ( Board newBoard, Player newPlayer ) {
        isRoundOver = false;

        board = newBoard;
        player = newPlayer;

        playerByID = player.playerID;
    }

    // broken in it's current implementation
    public bool ExecuteTurn ( Tile selectedTile ) {
        Debug.Log ( "[PlayerTurnController][ExecuteTurn] Executing Turn." );
        if ( !isRoundOver ) {
            Vector2 selectedTilePosition = selectedTile.ReturnTilePosition();
            if ( board.TileTable.ContainsKey ( selectedTilePosition ) ) {
                if ( selectedTile.isAValidMove == true ) {
                    Debug.Log ( "[PlayerTurnController][ExecuteTurn] Found a move." );
                    selectedTile.MarkTileAsSelected ( playerByID );
                    isRoundOver = player.UpdateMoveTable ( selectedTilePosition );
                    player.TakeTurn ( false );
                    return true;
                }
            }
        } else {
            Debug.Log ( "[PlayerTurnController][ExecuteTurn] Game is over." );
        }
        return false;
    }
}
