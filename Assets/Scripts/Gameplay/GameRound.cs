using UnityEngine;
using System.Collections;

public class GameRound {
    private GameObject boardObject;

    private GameManager gameManager;

    private Board board;
    private BoardManager boardManager;

    private PlayerInitialiser playerInitialiser;

    private int boardWidth;
    private int boardHeight;

    public GameRound() {
        gameManager = MonoBehaviour.FindObjectOfType<GameManager> ( );
        boardManager = MonoBehaviour.FindObjectOfType<BoardManager> ( );

        boardObject = MonoBehaviour.Instantiate ( Resources.Load<GameObject> ( ResourcePath.board ) as GameObject );
        boardObject.transform.SetParent ( boardManager.transform );

        boardWidth = 3;
        boardHeight = 3;

        board = boardObject.GetComponent<Board> ( );
        board.CreateBoard ( boardObject, board, boardManager , boardWidth , boardHeight );

        playerInitialiser = new PlayerInitialiser ( );

        gameManager.SetGameParams ( board , playerInitialiser.GetPlayers ( ), playerInitialiser.RandomizeStartingPlayer ( ) );

        MainCamera.SetCameraPosition ( );
    }
}
