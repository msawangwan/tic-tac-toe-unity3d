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
    private List<PlayerData> playerData;
    private List<bool> playerControlType;

    private int currentGamePlayerCount = 0;

    private bool isConfigured;

    /* Constructor takes a list of bools. Each bool 
        signifies if player is of type 'human' or 'ai' */
    public PlayerConfiguration( List<bool> controlType ) {
        isConfigured = false;
        playerData = new List<PlayerData> ( );
        playerControlType = new List<bool> ( );

        playerData.Clear ( );
        playerControlType.Clear ( );

        playerControlType = controlType;
    }

    /* Returns a copy of the current player data. */
    public List<PlayerData> GetPlayerData() {
        if ( isConfigured == false ) { // will be false first time this method is called
            playerData = InstantiatePlayerObjects ( );
            isConfigured = true;
        }
        return playerData;
    }

    /* Instantiate player GameObjects in  
        the scene and init their data. */
    private List<PlayerData> InstantiatePlayerObjects () {
        List<PlayerData> playerDataList = new List<PlayerData>();

        /* Possible player control types. */
        GameObject human = Resources.Load<GameObject> ( ResourcePath.playerHuman );
        GameObject ai = Resources.Load<GameObject> ( ResourcePath.playerAI );

        playerDataList.Clear ( );

        for ( int id = 0; id < playerControlType.Count; id++ ) {
            GameObject player;
            bool isHuman;
            string playerName = "";

            if ( playerControlType[id] ) {
                isHuman = true;
                player = MonoBehaviour.Instantiate<GameObject> ( human );
                playerName = "Human";
            } else {
                isHuman = false;
                player = MonoBehaviour.Instantiate<GameObject> ( ai );
                playerName = "The AI";
            }

            if ( player != null ) {
                PlayerContainer.AttachToTransformAsChild ( player );
                playerDataList.Add ( new PlayerData ( player , player.GetComponent<Player> ( ) , id , isHuman, playerName ) );
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
public class PlayerData {
    public GameObject PlayerObject { get; private set; }
    public Player PlayerReference { get; private set; }
    public int ID { get; private set; }
    public bool IsHuman { get; private set; }
    public string PlayerName { get; private set; }

    public PlayerData ( GameObject playerObject, Player playerReference, int id, bool isHuman, string pname ) {
        PlayerObject = playerObject;
        PlayerReference = playerReference;
        ID = id;
        IsHuman = isHuman;
        PlayerName = pname;

        playerReference.InitAsNew ( ID, PlayerName );
    }
}