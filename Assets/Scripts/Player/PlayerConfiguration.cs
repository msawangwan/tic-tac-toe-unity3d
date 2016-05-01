using UnityEngine;
using System;
using System.Collections.Generic;

/// <summary>
/// Configures a player.
/// 
/// A player can be either human or AI - this class is accompanied
/// by a sister class PlayerObjectData which acts as a data container
/// that wraps the important player references.
/// </summary>
public class PlayerConfiguration {
    private List<PlayerObjectData> playerData;
    private List<bool> playerControlType;

    private int currentGamePlayerCount = 0;

    private bool isConfigured;

    /* Constructor takes a list of bools. Each bool 
        signifies if player is of type 'human' or 'ai' */
    public PlayerConfiguration( List<bool> controlType ) {
        isConfigured = false;
        playerData = new List<PlayerObjectData> ( );
        playerControlType = new List<bool> ( );

        playerData.Clear ( );
        playerControlType.Clear ( );

        playerControlType = controlType;
    }

    /* Returns a copy of the current player data. */
    public List<PlayerObjectData> GetPlayerData() {
        if ( isConfigured == false ) { // will be false first time this method is called
            playerData = InstantiatePlayerObjects ( );
            isConfigured = true;
        }
        return playerData;
    }

    /* Instantiate player GameObjects in  
        the scene and init their data. */
    private List<PlayerObjectData> InstantiatePlayerObjects () {
        List<PlayerObjectData> playerDataList = new List<PlayerObjectData>();

        /* Possible player control types. */
        GameObject human = Resources.Load<GameObject> ( ResourcePath.playerHuman );
        GameObject ai = Resources.Load<GameObject> ( ResourcePath.playerAI );

        playerDataList.Clear ( );

        for ( int id = 0; id < playerControlType.Count; id++ ) {
            GameObject player;
            Player playerReference; // TODO: if 'player.GetComponent<Player>' works, delete this ref
            bool isHuman;
            string playerName = "";

            if ( playerControlType[id] ) {
                isHuman = true;
                player = MonoBehaviour.Instantiate<GameObject> ( human );
                playerReference = player.GetComponent<PlayerHuman> ( );
                playerName = "Human";
            } else {
                isHuman = false;
                player = MonoBehaviour.Instantiate<GameObject> ( ai );
                playerReference = player.GetComponent<PlayerComputer> ( );
                playerName = "The AI";
            }

            //playerReference.InitAsNew ( id , "DELETE MEEEEEE" );

            if ( player != null ) {
                PlayerContainer.AttachToTransformAsChild ( player );
                playerDataList.Add ( new PlayerObjectData ( player , player.GetComponent<Player> ( ) , id , isHuman, playerName ) );
                ++currentGamePlayerCount;
            } else {
                Debug.Log ( "[PlayerConfiguration][InstantiatePlayerObjects] GameObject 'player' is null " );
            }
        }
        return playerDataList;
    }

    /* Static method, on call will return a new turn-based state machine. */
    public static PlayerTurnSystem InstantiatePlayerTurnBasedMachine () {
        GameObject turnMachine = new GameObject("Player Turn System");
        PlayerContainer.AttachToTransformAsChild ( turnMachine );

        return turnMachine.AddComponent<PlayerTurnSystem> ( );
    }
}

/// <summary>
/// Related sister class:
/// Small object that packages important player data
/// </summary>
public class PlayerObjectData {
    public GameObject PlayerObject { get; private set; }
    public Player PlayerReference { get; private set; }
    public int ID { get; private set; }
    public bool IsHuman { get; private set; }
    public string PlayerName { get; private set; }

    public PlayerObjectData ( GameObject playerObject, Player playerReference, int id, bool isHuman, string pname ) {
        PlayerObject = playerObject;
        PlayerReference = playerReference;
        ID = id;
        IsHuman = isHuman;
        PlayerName = pname;

        playerReference.InitAsNew ( ID, PlayerName );
    }
}