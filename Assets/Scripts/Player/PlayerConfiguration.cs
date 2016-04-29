using UnityEngine;
using System.Collections.Generic;

public class PlayerObjData {
    public GameObject PlayerObject { get; set; }
    public Player PlayerReference { get; set; }
}

public class PlayerConfiguration : IConfigureable {
    private Dictionary<int, PlayerObjData> players;
    private Dictionary<int,bool> playerTypes;

    public PlayerConfiguration( Dictionary<int,bool> playerTypes ) {
        players = new Dictionary<int , PlayerObjData> ( );
        playerTypes = new Dictionary<int , bool> ( );

        players.Clear ( );
        playerTypes.Clear ( );

        this.playerTypes = playerTypes;
    }

    // implements 'IConfigureable'
    public Configuration Configure () {
        InstantiatePlayers ( );
        return new Configuration (  );
    }

    private void InstantiatePlayers() {
        Resources.Load<GameObject> ( ResourcePath.playerHuman );
        Resources.Load<GameObject> ( ResourcePath.playerAI );

        for ( int id = 0; id < players.Count; id++ ) {
            GameObject current;
            PlayerObjData newPlayer;

            if ( playerTypes[id] ) // is a human player
                current = MonoBehaviour.Instantiate<GameObject> ( Resources.Load<GameObject> ( ResourcePath.playerHuman ) );
            else                   // is an AI player
                current = MonoBehaviour.Instantiate<GameObject> ( Resources.Load<GameObject> ( ResourcePath.playerAI ) );
            PlayerContainer.AttachToTransformAsChild ( current );

            newPlayer = new PlayerObjData { PlayerObject = current, PlayerReference = current.GetComponent<Player>() };
            newPlayer.PlayerReference.Init ( id );

            players.Add ( id, newPlayer );
        }
    }
}
