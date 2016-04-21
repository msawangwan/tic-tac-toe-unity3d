using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public enum PlayerID {
    player1 = 0,
    player2 = 1,
}

public class PlayerInitialiser {
    private GameObject playerObj1;
    private GameObject playerObj2;

    private PlayerManager playerManager;
    private List<Player> players;
    private Player player1;
    private Player player2;

    private Board playerBoardReference;

    public PlayerInitialiser( PlayerManager manager, Board boardReference ) {
        playerManager = manager;
        playerBoardReference = boardReference;

        InstantiatePlayers ( );
    }

    public List<Player> GetPlayers() {
        return players;
    }

    private void InstantiatePlayers() {
        playerObj1 = MonoBehaviour.Instantiate<GameObject> ( Resources.Load ( ResourcePath.playerHuman ) as GameObject );
        playerObj2 = MonoBehaviour.Instantiate<GameObject> ( Resources.Load ( ResourcePath.playerAI ) as GameObject );

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

        RandomizeStartingPlayer ( );
    }

    private void RandomizeStartingPlayer ( ) {
        int coinFlip = UnityEngine.Random.Range( 0, 2 );

        if ( coinFlip == 0 )
            player1.MoveFirst ( true );
        else
            player2.MoveFirst ( true );
    }
}
