using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
    private Board board;

    private PlayerID currentPlayer;
    private Player player;
    private Player player_cpu;
    private List<Player> playerList;

    public bool isGameOver { get; private set; }

    public void SetGameParams ( Board board , List<Player> players, PlayerID startingPlayer ) {
        isGameOver = false;

        this.board = board;
        player = players[0];
        player_cpu = players[1];

        player.SetInitialTurn ( startingPlayer );
        player_cpu.SetInitialTurn ( startingPlayer );
    }

    public bool MakeMove( Tile selectedTile, PlayerID playerID ) {
        if (!isGameOver) {
            currentPlayer = playerID;
            Vector2 selectedTilePosition = selectedTile.ReturnTilePosition();
            if (board.TileTable.ContainsKey( selectedTilePosition )) {
                if ( selectedTile.isAValidMove == true) {
                    selectedTile.MarkTileAsSelected( currentPlayer );
                    CheckWinCondition( currentPlayer, selectedTilePosition );
                    FinishPlayerTurn( currentPlayer );
                    return true;
                }
            }
        } else {
            Debug.Log( "[GAME MANAGER] Game is over ... " );
        }
        return false;
    }

    private void FinishPlayerTurn( PlayerID playerMoved ) {
        if (playerMoved == (PlayerID) 0) {
            player.EndTurn( );
            player_cpu.StartTurn( );
        } else {
            player_cpu.EndTurn( );
            player.StartTurn( );
        }
    }

    private void CheckWinCondition(PlayerID currentPlayer, Vector2 currentMove) {
        string theWinner = "WINNER IS ";
        if (currentPlayer == (PlayerID) 0) {
            isGameOver = player.UpdateMoveTable( currentMove );
            theWinner += "PLAYER 1";
        } else {
            isGameOver = player_cpu.UpdateMoveTable( currentMove );
            theWinner += "PLAYER 2";
        }

        if(isGameOver) {
            Debug.Log( "GAME OVER" );
            Debug.Log( theWinner );
        }
    }
}

