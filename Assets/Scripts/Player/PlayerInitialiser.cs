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

    private PlayerID playerToAct;

    private PlayerManager manager;
    private List<Player> players;
    private Player player1;
    private Player player2;

    public PlayerInitialiser() {        
        InstantiatePlayers ( );
    }

    public List<Player> GetPlayers() {
        return players;
    }

    public PlayerID RandomizeStartingPlayer ( ) {
        int coinFlip = UnityEngine.Random.Range( 0, 2 );
        return ( PlayerID ) coinFlip; ;
    }

    private void InstantiatePlayers() {
        manager = MonoBehaviour.FindObjectOfType<PlayerManager> ( );

        playerObj1 = MonoBehaviour.Instantiate<GameObject> ( Resources.Load ( ResourcePath.playerHuman ) as GameObject );
        playerObj2 = MonoBehaviour.Instantiate<GameObject> ( Resources.Load ( ResourcePath.playerAI ) as GameObject );

        playerObj1.transform.SetParent ( manager.transform );
        playerObj2.transform.SetParent ( manager.transform );

        players = new List<Player> ( );
        players.Clear ( );

        player1 = playerObj1.GetComponent<PlayerHuman> ( ) as Player;
        player2 = playerObj2.GetComponent<PlayerComputer> ( ) as Player;

        player1.playerID = 0;
        player2.playerID = ( PlayerID ) 1;

        players.Add ( player1 );
        players.Add ( player2 );
    }
}
