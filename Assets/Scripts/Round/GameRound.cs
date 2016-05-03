using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameRound : IRound {
    private Grid2DObjectData gridData;

    private List<bool> controlTypeOfPlayer = new List<bool> ( );

    private PlayerTurnSystem turnMachine;
    private PlayerObjectData p1Data;
    private PlayerObjectData p2Data;
    private List<PlayerObjectData> playerData;

    public string RoundWinner { get; private set; }

    public IEnumerable LoadedTransitionIntroAsset { get; private set; }
    public IEnumerable LoadedTransitionOutroAsset { get; private set; }

    public bool IsGameOver { get; private set; }

    /* Constructor */
    public GameRound() { }

    public void AddPlayerControlType ( bool pType ) {
        controlTypeOfPlayer.Add ( pType );
    }

    public void StartNewRound() {
        IsGameOver = false;
        RoundWinner = "";

        foreach ( PlayerObjectData p in playerData ) {
            p.PlayerReference.NewGameState ( );
        }

        turnMachine.StartFirstTurn ( );
    }

    public void EndCurrentRound ( string winner ) {
        IsGameOver = true;
        RoundWinner = winner;
    }

    public void LoadNewGrid ( ) {
        InstantiateGrid ( );
        InstantiateGridTiles ( );

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

    public void LoadTurns ( ) {
        turnMachine = PlayerConfiguration.InstantiatePlayerTurnBasedMachine ( );

        int coinFlip = UnityEngine.Random.Range( 0, 2 );
        if ( coinFlip == 0 )
            turnMachine.SetStartingPlayer ( this , p1Data.PlayerReference );
        else
            turnMachine.SetStartingPlayer ( this , p2Data.PlayerReference );
    }

    private void InstantiateGrid ( ) {
        Grid2DConfiguration gridConfig = DataInstantiator.GetNewInstance( () => new Grid2DConfiguration (3, 3));
        gridData = gridConfig.GetGrid2DData ( );
    }

    private void InstantiateGridTiles ( ) {
        Grid2DTiledBoard grid = gridData.GridObject.AddComponent<Grid2DTiledBoard> ( );
        grid.LayTilesOnGrid ( );
        LoadedTransitionIntroAsset = grid.DrawTiles ( ); // .34f
        LoadedTransitionOutroAsset = grid.FadeOut ( );
    }
}
