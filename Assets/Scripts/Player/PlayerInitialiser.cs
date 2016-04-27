using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PlayerInitialiser {
    private GameObject playerTurnStateMachineObj;
    private GameObject playerObj1;
    private GameObject playerObj2;

    private PlayerManager playerManager;
    private PlayerTurnStateMachine playerTurnStateMachine;
    private List<Player> players;
    private Player player1;
    private Player player2;

    private Board playerBoardReference;

    public PlayerInitialiser( PlayerManager manager, Board boardReference ) {
        playerManager = manager;
        playerBoardReference = boardReference;

        InstantiatePlayers ( );
        InitialisePlayerTurns ( );
    }

    public List<Player> GetPlayers() {
        return players;
    }

    private void InstantiatePlayers() {
        playerObj1 = MonoBehaviour.Instantiate<GameObject> ( Resources.Load<GameObject> ( ResourcePath.playerHuman ) );
        playerObj2 = MonoBehaviour.Instantiate<GameObject> ( Resources.Load<GameObject> ( ResourcePath.playerAI ) );

        playerObj1.transform.SetParent ( playerManager.transform );
        playerObj2.transform.SetParent ( playerManager.transform );

        players = new List<Player> ( );
        players.Clear ( );

        players.Add ( player1 );
        players.Add ( player2 );

        player1 = playerObj1.GetComponent<PlayerHuman> ( ) as Player;
        player2 = playerObj2.GetComponent<PlayerComputer> ( ) as Player;

        player1.InitPlayer ( playerBoardReference , 0 );
        player2.InitPlayer ( playerBoardReference , ( PlayerID ) 1 );
    }

    private void InitialisePlayerTurns ( ) {
        int coinFlip = UnityEngine.Random.Range( 0, 2 );
        playerTurnStateMachineObj = MonoBehaviour.Instantiate<GameObject> (Resources.Load<GameObject> (ResourcePath.playerTurnStateMachine) );
        playerTurnStateMachine = playerTurnStateMachineObj.GetComponent<PlayerTurnStateMachine> ( );
        if ( coinFlip == 0 )
            playerTurnStateMachine.InitPlayerPlayMachine ( player1 );
        else
            playerTurnStateMachine.InitPlayerPlayMachine ( player2 );
    }
}
