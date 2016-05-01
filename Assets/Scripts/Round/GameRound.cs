using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameRound : IRound {
    private Grid2DObjectData gridData;

    private List<bool> controlTypeOfPlayer = new List<bool> ( );
    private List<PlayerObjectData> playerData;
    private PlayerObjectData p1Data;
    private PlayerObjectData p2Data;

    private PlayerTurnStateMachine turnState;

    public IEnumerable LoadedTransitionAsset { get; private set; }

    public bool IsGameOver { get; private set; }

    public GameRound() {

    }

    public void StartNewRound() {
        IsGameOver = false;
        turnState.StartFirstTurn ( );
        Debug.Log ( "GGAAAAMMMERROUND" );
    }

    public void EndCurrentRound() {
        IsGameOver = true;
    }

    public void AddPlayerControlType( bool pType ) {
        controlTypeOfPlayer.Add ( pType );
    }

    public void LoadNewGrid ( ) {
        LoadGrid ( );
        LoadGridTiles ( );

        MainCamera.SetCameraPosition ( gridData.GridReference );
    }

    public void LoadPlayers ( ) {
        playerData = new List<PlayerObjectData> ( );
        playerData.Clear ( );

        PlayerConfiguration playerConfig = DataInstantiator.GetNewInstance( () => new PlayerConfiguration ( controlTypeOfPlayer ) );

        playerData = playerConfig.GetPlayerData ( );
        p1Data = playerData[0];
        p2Data = playerData[1];
    }

    private void LoadGrid ( ) {
        Grid2DConfiguration gridConfig = DataInstantiator.GetNewInstance( () => new Grid2DConfiguration (3, 3));
        gridData = gridConfig.GetGrid2DData ( );
    }

    private void LoadGridTiles ( ) {
        Grid2DTicTacToe board = gridData.GridObject.AddComponent<Grid2DTicTacToe> ( );
        board.LayTilesOnGrid ( );
        LoadedTransitionAsset = board.DrawTiles ( .34f );
    }

    // TODO FIX THIS WAS COPY AND PASTED
    public void LoadTurns ( ) {
        // TODO: use static addtotransform class in gamemanager class, currently no parent transform set

        GameObject turnMachine = new GameObject();
        turnState = turnMachine.AddComponent<PlayerTurnStateMachine> ( );
        p2Data.PlayerObject.GetComponent<PlayerComputer> ( ).InitAi ( ); // find somewhere else to do
        int coinFlip = UnityEngine.Random.Range( 0, 2 );
        if ( coinFlip == 0 )
            turnState.InitPlayerPlayMachine ( this , p1Data.PlayerReference );
        else
            turnState.InitPlayerPlayMachine ( this , p2Data.PlayerReference );
    }
}
