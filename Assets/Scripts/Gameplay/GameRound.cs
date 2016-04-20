using UnityEngine;
using System.Collections;

public class GameRound {
    private GameManager gameManager;
    private PlayerManager playerManager;
    private BoardManager boardManager;

    private GameObject boardObject;
    private Board board;

    private PlayerInitialiser playerInitialiser;
    private GameTurn turns;

    private int boardWidth;
    private int boardHeight;

    public bool roundOver { get; private set; }

    public GameRound( PlayerInitialiser newPlayers, GameTurn newTurns ) {
        gameManager = MonoBehaviour.FindObjectOfType<GameManager> ( );
        boardManager = MonoBehaviour.FindObjectOfType<BoardManager> ( );
        playerManager = MonoBehaviour.FindObjectOfType<PlayerManager> ( );

        boardObject = MonoBehaviour.Instantiate ( Resources.Load<GameObject> ( ResourcePath.board ) as GameObject );
        boardObject.transform.SetParent ( boardManager.transform );

        boardWidth = 3;
        boardHeight = 3;

        board = boardObject.GetComponent<Board> ( );
        board.CreateBoard ( boardObject, board, boardManager , boardWidth , boardHeight );

        playerInitialiser = newPlayers;
        turns = newTurns;

        turns.InitialiseTurns ( playerInitialiser.GetPlayers ( ) , playerInitialiser.GetRandomPlayerByID ( ) );

        MainCamera.SetCameraPosition ( );
    }

    public void StartNewRound() {
        //TODO
    }
}
