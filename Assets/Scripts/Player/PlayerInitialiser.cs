using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PlayerInitialiser {
    private IRound currentRount;

    private GameObject playerObj1;
    private GameObject playerObj2;
    private GameObject playerTurnStateMachineObj;
    
    private Player player1Ref;
    private Player player2Ref;   
    private PlayerTurnStateMachine turnStateMachineRef;
    private Board gameBoardRef;

    private List<Player> players;

    public PlayerInitialiser( IRound newRound, Board boardReference ) {
        currentRount = newRound; 
        gameBoardRef = boardReference;

        InstantiatePlayers ( );
        InitialisePlayers ( );
        InitialisePlayerTurnState ( );
    }

    // call to start the round, refactor -- too many dependencies
    public void PlayersReadyStartRound() {
        turnStateMachineRef.StartNewRound ( );
    }

    public List<Player> FetchPlayerList() {
        return players;
    }

    // TODO: make it so state carrys over between multiple rounds
    private void InstantiatePlayers() {
        if (playerObj1 == null) {
            playerObj1 = MonoBehaviour.Instantiate<GameObject> ( Resources.Load<GameObject> ( ResourcePath.playerHuman ) );
            player1Ref = playerObj1.GetComponent<PlayerHuman> ( ) as Player;
            PlayerContainer.AttachToTransformAsChild ( playerObj1 );
        }

        if (playerObj2 == null) {
            playerObj2 = MonoBehaviour.Instantiate<GameObject> ( Resources.Load<GameObject> ( ResourcePath.playerAI ) );
            player2Ref = playerObj2.GetComponent<PlayerComputer> ( ) as Player;
            PlayerContainer.AttachToTransformAsChild ( playerObj2 );
        }

        players = new List<Player> ( );
        players.Clear ( );

        players.Add ( player1Ref );
        players.Add ( player2Ref );
    }

    private void InitialisePlayers() {
        for ( int i = 0; i < players.Count; i++ ) {
            players[i].InitPlayer ( gameBoardRef , i );
        }
    }

    private void InitialisePlayerTurnState ( ) {
        // TODO: use static addtotransform class in gamemanager class, currently no parent transform set
        playerTurnStateMachineObj = MonoBehaviour.Instantiate<GameObject> (Resources.Load<GameObject> (ResourcePath.playerTurnStateMachine) );
        turnStateMachineRef = playerTurnStateMachineObj.GetComponent<PlayerTurnStateMachine> ( );

        int coinFlip = UnityEngine.Random.Range( 0, 2 );
        if ( coinFlip == 0 )
            turnStateMachineRef.InitPlayerPlayMachine ( currentRount, player1Ref );
        else
            turnStateMachineRef.InitPlayerPlayMachine ( currentRount, player2Ref );
    }
}
