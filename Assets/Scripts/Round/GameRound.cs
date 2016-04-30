using UnityEngine;
using System.Collections.Generic;

public class GameRound : IRound {
    private int playerCount = 0;

    private Grid2DObjectData gridData;

    private List<PlayerObjectData> playerData;
    private PlayerObjectData p1Data;
    private PlayerObjectData p2Data;

    public bool IsGameOver { get; private set; }

    public GameRound() {
        playerData = new List<PlayerObjectData> ( );

        playerData.Clear ( );
    }

    public void StartNewRound() {
        IsGameOver = false;
        CreateGameBoard ( );
        //MainCamera.SetCameraPosition ( );
    }

    public void EndCurrentRound() {
        IsGameOver = true;
    }

    void SetPlayersInRound ( List<bool> controlTypeOfPlayer ) {
        PlayerConfiguration playerConfig = DataInstantiator.GetNewInstance( () => new PlayerConfiguration ( controlTypeOfPlayer ) );
        playerData = playerConfig.GetPlayerData ( );
        p1Data = playerData[0];
        p2Data = playerData[1];
        // TODO ...
    }

    void CreateGameBoard () {
        Grid2DConfiguration gridConfig = DataInstantiator.GetNewInstance( () => new Grid2DConfiguration (3, 3));
        Debug.Log ( "created gameboard" );
        gridData = gridConfig.GetGrid2DData ( );
    }

    // TODO FIX THIS WAS COPY AND PASTED
    private void InitialisePlayerTurnState ( ) {
        // TODO: use static addtotransform class in gamemanager class, currently no parent transform set
        GameObject playerTurnStateMachineObj;
        PlayerTurnStateMachine turnStateMachineRef;
        playerTurnStateMachineObj = MonoBehaviour.Instantiate<GameObject> ( Resources.Load<GameObject> ( ResourcePath.playerTurnStateMachine ) );
        turnStateMachineRef = playerTurnStateMachineObj.GetComponent<PlayerTurnStateMachine> ( );

        int coinFlip = UnityEngine.Random.Range( 0, 2 );
        if ( coinFlip == 0 )
            turnStateMachineRef.InitPlayerPlayMachine ( this , p1Data.PlayerReference );
        else
            turnStateMachineRef.InitPlayerPlayMachine ( this , p2Data.PlayerReference );
    }
}
