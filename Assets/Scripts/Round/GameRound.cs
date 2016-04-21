using UnityEngine;
using System.Collections;

public class GameRound {
    private GameManager gameManager;
    private PlayerManager playerManager;
    private BoardManager boardManager;

    private GameObject boardObject;
    private Board board;

    private PlayerInitialiser playerInitialiser;

    private int boardWidth;
    private int boardHeight;

    public bool roundOver { get; private set; }

    public static GameRound StartNewRound ( ) {
        return new GameRound ( );
    }

    private GameRound( ) {
        gameManager = MonoBehaviour.FindObjectOfType<GameManager> ( );
        boardManager = MonoBehaviour.FindObjectOfType<BoardManager> ( );
        playerManager = MonoBehaviour.FindObjectOfType<PlayerManager> ( );

        boardObject = MonoBehaviour.Instantiate ( Resources.Load<GameObject> ( ResourcePath.board ) as GameObject );
        boardObject.transform.SetParent ( boardManager.transform );

        boardWidth = 3;
        boardHeight = 3;

        board = boardObject.GetComponent<Board> ( );
        board.CreateBoard ( boardObject, board, boardManager , boardWidth , boardHeight );

        playerInitialiser = new PlayerInitialiser( playerManager, board );

        MainCamera.SetCameraPosition ( );
    }
}
