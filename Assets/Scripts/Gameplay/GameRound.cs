using UnityEngine;
using System.Collections;

public class GameRound {
    private GameObject boardObject;

    private Board board;
    private BoardManager boardManager;

    private int boardWidth;
    private int boardHeight;

    public GameRound() {
        boardManager = MonoBehaviour.FindObjectOfType<BoardManager> ( );

        boardObject = MonoBehaviour.Instantiate ( Resources.Load<GameObject> ( ResourcePath.board ) as GameObject );
        boardObject.transform.SetParent ( boardManager.transform );

        boardWidth = 3;
        boardHeight = 3;

        board = boardObject.GetComponent<Board> ( );
        board.CreateBoard ( boardObject, board, boardManager , boardWidth , boardHeight );

        MainCamera.SetCameraPosition ( );
    }
}
