using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public enum PlayerID {
    player1 = 0,
    player2 = 1,
}

public class GameManager : MonoBehaviour {
    private StateManager statemanager;
    private State currentState;

    private UIManager ui;
    private Board board;

    private PlayerID currentPlayer;
    private Player player;
    private Player player_cpu;
    private List<Player> playerList;

    private bool isPlayersReset = false;
    private bool isGameOver = true;

    public void StartGameManager( ) {
        GetReferences( );
        RunSetUp( );
    }

    public void SetState(State newState) {
        currentState = newState;
    }

    public bool ResetPlayers() {
        if( isPlayersReset == false ) {       // no game currently active
            InitialisePlayersForNewRound ( );
            return true;                      // return true if successful
        } else {
            return false;
        }
    }

    public bool MakeMove( Tile selectedTile, PlayerID playerID ) {
        if (!isGameOver) {
            currentPlayer = playerID;
            Vector2 selectedTilePosition = selectedTile.ReturnTilePosition();
            if (board.MoveTable.ContainsKey( selectedTilePosition )) {
                if (board.MoveTable[selectedTilePosition] == true) {
                    board.MoveTable[selectedTilePosition] = false;
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

    private void Update() {
        //Debug.Log ( "[GAME MANAGER] Current gamestate: " + currentState.GetType ( ) );
    }

    private void GetReferences() {
        board = FindObjectOfType<Board>( );
        ui = FindObjectOfType<UIManager>( );
        statemanager = FindObjectOfType<StateManager> ( );
        player = FindObjectOfType<PlayerHuman>( );
        player_cpu = FindObjectOfType<PlayerComputer>( );
    }

    private void RunSetUp ( ) {
        InitialGameState ( );
    }

    private void InitialGameState() {
        currentState = statemanager.SetInitialGameState ( );
        currentState.UpdateState ( );
    }

    private void InitialisePlayersForNewRound() {
        playerList = new List<Player>( );
        playerList.Clear( );
        playerList.Add(player);
        playerList.Add(player_cpu);

        foreach(Player p in playerList.Where(ply => ply.isTurn == false)) {
            Debug.Log(p.name);
        }

        currentPlayer = ChooseStartingPlayerRandom( );   // randomly choose player to move first
        
        for ( int i = 0; i < playerList.Count; i++ ) {   // assign ID (do this somewhere else?) & notify each player of who is starting first
            if ( Enum.IsDefined(typeof(PlayerID), i) ) {
                playerList[i].playerID = (PlayerID) i;
                playerList[i].SetInitialTurn(currentPlayer);
            } else {
                Debug.Log("[GAME MANAGER] Not a valid player ID");
            }
        }

        isPlayersReset = true;
    }

    private PlayerID ChooseStartingPlayerRandom() {
        int coinFlip = UnityEngine.Random.Range( 0, 2 );
        print( "starting player is " + coinFlip );
        return (PlayerID) coinFlip; ;
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

