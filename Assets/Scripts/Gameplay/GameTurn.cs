using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameTurn {
    private Board board;
    //private GameRound round;

    private PlayerID currentPlayer;
    private Player player;
    private Player player_cpu;
    private List<Player> playerList;

    public bool isRoundOver { get; private set; }

    public GameTurn ( Board newBoard ) {
        isRoundOver = false;

        //round = newRound;
        board = newBoard;
    }

    // called right before a new round
    public void InitialiseTurns( List<Player> players , PlayerID startingPlayer ) {
        player = players[0];
        player_cpu = players[1];

        player.SetInitialTurn ( startingPlayer );
        player_cpu.SetInitialTurn ( startingPlayer );
    }

    public bool ExecuteTurn ( Tile selectedTile , PlayerID playerID ) {
        if ( !isRoundOver ) {
            currentPlayer = playerID;
            Vector2 selectedTilePosition = selectedTile.ReturnTilePosition();
            if ( board.TileTable.ContainsKey ( selectedTilePosition ) ) {
                if ( selectedTile.isAValidMove == true ) {
                    selectedTile.MarkTileAsSelected ( currentPlayer );
                    CheckWinCondition ( currentPlayer , selectedTilePosition );
                    FinishPlayerTurn ( currentPlayer );
                    return true;
                }
            }
        } else {
            Debug.Log ( "[Turns][ExecuteTurn] Game is over." );
        }
        return false;
    }

    private void FinishPlayerTurn ( PlayerID playerMoved ) {
        if ( playerMoved == ( PlayerID ) 0 ) {
            player.EndTurn ( );
            player_cpu.StartTurn ( );
        } else {
            player_cpu.EndTurn ( );
            player.StartTurn ( );
        }
    }

    private void CheckWinCondition ( PlayerID currentPlayer , Vector2 currentMove ) {
        string theWinner = "WINNER IS ";
        if ( currentPlayer == ( PlayerID ) 0 ) {
            isRoundOver = player.MakeMove ( currentMove );
            theWinner += "PLAYER 1";
        } else {
            isRoundOver = player_cpu.MakeMove ( currentMove );
            theWinner += "PLAYER 2";
        }

        if ( isRoundOver ) {
            Debug.Log ( "GAME OVER" );
            Debug.Log ( theWinner );
        }
    }
}
