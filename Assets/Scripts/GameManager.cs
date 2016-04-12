using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public enum PlayerID {
    player1 = 0,
    player2 = 1,
}

public class GameManager : MonoBehaviour {
    private Board gameboard;
    private UIManager ui;
    private Player player;
    private Player cpu;
    private List<Player> playerList;
    private PlayerID currentPlayersTurn;
    private bool isGameOver = false;

    public void StartGameManager( ) {
        Initialise( );
    }

    public bool MakeMove( Tile selectedTile, PlayerID playerID ) {
        if (!isGameOver) {
            currentPlayersTurn = playerID;
            Vector2 selectedTilePosition = selectedTile.ReturnTilePosition();
            if (gameboard.MoveTable.ContainsKey( selectedTilePosition )) {
                if (gameboard.MoveTable[selectedTilePosition] == true) {
                    gameboard.MoveTable[selectedTilePosition] = false;
                    selectedTile.MarkTileAsSelected( currentPlayersTurn );
                    CheckWinCondition( currentPlayersTurn, selectedTilePosition );
                    FinishPlayerTurn( currentPlayersTurn );
                    return true;
                }
            }
        } else {
            Debug.Log( "[GAME MANAGER] Game is over ... " );
        }
        return false;
    }

    private void Update() {
        if(!isGameOver) {
            Debug.Log( "[GAME MANAGER] Current player's turn is " + currentPlayersTurn );
        }
    }

    private void Initialise() {
        gameboard = FindObjectOfType<Board>( );
        ui = FindObjectOfType<UIManager>( );
        player = FindObjectOfType<PlayerHuman>( );
        cpu = FindObjectOfType<PlayerComputer>( );

        playerList = new List<Player>( );
        playerList.Clear( );
        playerList.Add( player );
        playerList.Add( cpu );

        currentPlayersTurn = ChooseStartingPlayerRandom( );

        for (int i = 0; i < playerList.Count; i++) {
            if (Enum.IsDefined(typeof(PlayerID), i)) {
                playerList[i].playerID = (PlayerID) i;
                playerList[i].SetInitialTurn( currentPlayersTurn );
            } else {
                Debug.Log( "[GAME MANAGER] Not a valid player ID" );
            }
        }
    }

    private PlayerID ChooseStartingPlayerRandom() {
        int coinFlip = UnityEngine.Random.Range( 0, 2 );
        print( "starting player is " + coinFlip );
        return (PlayerID) coinFlip; ;
    }

    private void FinishPlayerTurn( PlayerID playerMoved ) {
        if (playerMoved == (PlayerID) 0) {
            player.EndTurn( );
            cpu.StartTurn( );
        } else {
            cpu.EndTurn( );
            player.StartTurn( );
        }
    }

    private void CheckWinCondition(PlayerID currentPlayer, Vector2 currentMove) {
        string theWinner = "WINNER IS ";
        if (currentPlayer == (PlayerID) 0) {
            isGameOver = player.UpdateMoveTable( currentMove );
            theWinner += "PLAYER 1";
        } else {
            isGameOver = cpu.UpdateMoveTable( currentMove );
            theWinner += "PLAYER 2";
        }

        if(isGameOver) {
            Debug.Log( "GAME OVER" );
            Debug.Log( theWinner );
        }
    }
}
