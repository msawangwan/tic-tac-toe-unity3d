using UnityEngine;
using System.Collections;

public class GameRound : IRound {
    private GameObject gameBoardObj;
    // TODO: implement boardInitialiser class??
    private BoardManager boardManager;
    private Board gameBoardRef;

    private PlayerInitialiser playerInitialiser;

    private int boardWidth = 3;
    private int boardHeight = 3;

    public bool IsGameOver { get; private set; }

    public static GameRound LoadNewRound ( ) {
        return new GameRound ( );
    }

    public void StartNewRound(IRound roundToStart) {
        IsGameOver = false;
        gameBoardRef.CreateBoard ( gameBoardObj , gameBoardRef , boardManager , boardWidth , boardHeight );
        playerInitialiser = new PlayerInitialiser ( roundToStart, gameBoardRef );
        playerInitialiser.PlayersReadyStartRound ( );
        MainCamera.SetCameraPosition ( );
    }

    public void EndCurrentRound() {
        IsGameOver = true;
    }

    public GameObject FetchBoardObjectRefernce () {
        if ( gameBoardObj )
            return gameBoardObj;
        return null;
    }

    // private constructor, called when an instance is instantiated via static method
    private GameRound() {
        boardManager = MonoBehaviour.FindObjectOfType<BoardManager> ( );
        gameBoardObj = MonoBehaviour.Instantiate<GameObject> ( Resources.Load<GameObject> ( ResourcePath.board ) );
        gameBoardObj.transform.SetParent ( boardManager.transform );
        gameBoardRef = gameBoardObj.GetComponent<Board> ( );
    }
}
