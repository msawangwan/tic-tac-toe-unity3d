using UnityEngine;
using System.Collections;

public class GameRound : IRound {
    private IGameBoard board;
    private IConfigureable playerConfig;
    private IConfigureable gridData;

    public GameObject GridObject { get; private set; }

    public bool IsGameOver { get; private set; }

    public GameRound() {
        //this.board = board;
    }

    public void StartNewRound() {
        IsGameOver = false;
        //gameBoardRef.CreateBoard ( gameBoardObj , gameBoardRef , boardWidth , boardHeight );
        //playerInitialiser = new PlayerInitialiser ( roundToStart, gameBoardRef );
        //playerInitialiser.PlayersReadyStartRound ( );
        MainCamera.SetCameraPosition ( );
    }

    public void EndCurrentRound() {
        IsGameOver = true;
    }
}
