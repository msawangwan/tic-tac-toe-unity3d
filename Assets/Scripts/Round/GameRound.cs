using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameRound {
    public IEnumerable LoadedTransitionIntroAsset { get; private set; }
    public IEnumerable LoadedTransitionOutroAsset { get; private set; }

    public TicTacToeEngine Game { get; set; }

    public bool IsGameOver { get; private set; }

    private Grid2D gridData;

    private PlayerData p1Data;
    private PlayerData p2Data;
    private List<PlayerData> playerData;
    private List<bool> controlTypeOfPlayer = new List<bool> ( );

    /* Constructor */
    public GameRound() { }

    public void AddPlayerControlType ( bool pType ) {
        controlTypeOfPlayer.Add ( pType );
    }

    public void StartNewRound() {
        IsGameOver = false;

        foreach ( PlayerData p in playerData ) {
            p.PlayerReference.NewGameState ( );
        }

        Game = new TicTacToeEngine ( gridData.GridReference, p1Data.PlayerReference, p2Data.PlayerReference );
    }

    public void LoadNewGrid ( ) {
        InstantiateGrid ( );
        InstantiateGridTiles ( );

        MainCamera.SetCameraPosition ( gridData.GridReference );
    }

    public void LoadPlayers ( ) {
        playerData = new List<PlayerData> ( );
        playerData.Clear ( );

        PlayerConfiguration playerConfig = DataInstantiator.GetNewInstance( () => new PlayerConfiguration ( controlTypeOfPlayer ) );

        playerData = playerConfig.GetPlayerData ( );
        p1Data = playerData[0];
        p2Data = playerData[1];
    }

    private void InstantiateGrid ( ) {
        Grid2DConfiguration gridConfig = DataInstantiator.GetNewInstance( () => new Grid2DConfiguration (3, 3, true));
        gridData = gridConfig.GetGrid2DData ( );
    }

    private void InstantiateGridTiles ( ) {
        Grid2DRendererComponent grid = gridData.GridObject.AddComponent<Grid2DRendererComponent> ( );
        grid.LayTilesOnGrid ( );
        LoadedTransitionIntroAsset = grid.DrawTiles ( ); // .34f
        LoadedTransitionOutroAsset = grid.FadeOut ( );
    }
}