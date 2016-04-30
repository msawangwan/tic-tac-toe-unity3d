using UnityEngine;
using System.Collections.Generic;

public class PlayerConfiguration : IConfigureable {
    private Dictionary<int,bool> playerTypes;

    public List<IConfig> ObjectConfigData { get; private set; }

    public PlayerConfiguration( Dictionary<int,bool> playerTypes ) {
        ObjectConfigData = new List<IConfig> ( );
        playerTypes = new Dictionary<int , bool> ( );

        ObjectConfigData.Clear ( );
        playerTypes.Clear ( );

        this.playerTypes = playerTypes;
    }

    // implements 'IConfigureable'
    public List<IConfig> Configure() {
        InstantiatePlayerObjects ( );
        return ObjectConfigData;
    }

    /* Spawn initialised player gameobjects in  
        the scene and add them to a collection */
    private void InstantiatePlayerObjects() {
        GameObject human = Resources.Load<GameObject> ( ResourcePath.playerHuman );
        GameObject ai = Resources.Load<GameObject> ( ResourcePath.playerAI );

        for ( int id = 0; id < ObjectConfigData.Count; id++ ) {
            GameObject player;
            PlayerObjectData newPlayer;
            bool isHuman;

            if ( playerTypes[id] ) {
                isHuman = true;
                player = MonoBehaviour.Instantiate<GameObject> ( human );
            } else {
                isHuman = false;
                player = MonoBehaviour.Instantiate<GameObject> ( ai );
            }
            
            newPlayer = new PlayerObjectData { PlayerObject = player, PlayerReference = player.GetComponent<Player>(), ID = id, IsHuman = isHuman };
            ObjectConfigData.Add ( newPlayer );

            PlayerContainer.AttachToTransformAsChild ( player );
        }
    }
}
/// <summary>
/// Related sister class
/// </summary>
public class PlayerObjectData : IConfig {
    public GameObject PlayerObject { get; set; }
    public Player PlayerReference { get; set; }
    public int ID { get; set; }
    public bool IsHuman { get; set; }
}