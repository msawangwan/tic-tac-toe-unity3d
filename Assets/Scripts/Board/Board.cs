using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Board : MonoBehaviour {
    private GameObject boardObject;

    private Board board; // cache reference to self from parent gameObject
    private BoardManager manager;
    private BoardInitialiser boardUtility;

    private Dictionary<Vector2, Tile> tileTable;
    private Vector2[] tileCoordinates;

    public int Width { get; private set; }
    public int Height { get; private set; }

    public Vector2 BoardCenterPoint { get; private set; }

    public bool isBoardActive = false;

    // use as constructor
    public void CreateBoard( GameObject boardObj, Board boardScriptReference, BoardManager boardManager , int boardWidth, int boardHeight ) {
        manager = boardManager;
        boardObject = boardObj;
        board = boardScriptReference;

        Width = boardWidth;
        Height = boardHeight;

        boardUtility = new BoardInitialiser ( Width, Height );

        tileCoordinates = boardUtility.GetGridCoordinates ( );
        BoardCenterPoint = boardUtility.GameboardCenterPoint;

        tileTable = boardUtility.DrawBoard ( gameObject, tileCoordinates );

        isBoardActive = boardObject.activeInHierarchy;
    }
}
