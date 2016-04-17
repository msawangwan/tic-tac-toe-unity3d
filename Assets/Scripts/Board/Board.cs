using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Board : MonoBehaviour {
    public const int gridWidth = 3;//
    public const int gridHeight = 3;//

    public Dictionary<Vector2, Tile> TileTable { get; set; }//
    public Dictionary<Vector2, bool> MoveTable { get; set; }//
    public Vector2[] TileCoordinates { get; set; }//

    private Vector2 boardCenterPoint;///
    public Vector2 BoardCenterPoint {
        get {
            bool hasCalculated = false;
            if (!hasCalculated) {
                int numCoordinates = TileCoordinates.Length;
                Vector2[] coordinatesToSum = TileCoordinates;
                Vector2 sum = new Vector2(0,0);

                foreach (Vector2 coor in coordinatesToSum) {
                    sum += coor;
                }

                float centerX = sum.x;
                float centerY = sum.y;
                centerX = centerX / numCoordinates;
                centerY = centerY / numCoordinates;
                boardCenterPoint = new Vector2( centerX, centerY );
                hasCalculated = true;
            }
            return boardCenterPoint;
        }
    }//

    public void SetupBoard ( ) {//
        InitialiseContainers ( );//
        CalculateCoordinates ( );//
        InstantaiteTiles ( );//
    }
    private void InitialiseContainers() {//
        TileCoordinates = new Vector2[gridWidth * gridHeight];//
        TileTable = new Dictionary<Vector2, Tile>( );//
        MoveTable = new Dictionary<Vector2, bool>( );//
    }

    private void CalculateCoordinates( ) {//
        for (int i = 0, x = 0; x < gridWidth; x++) {//
            for (int y = 0; y < gridHeight; y++, i++) {//
                TileCoordinates[i] = new Vector2( x, y );//
            }
        }
    }

    private void InstantaiteTiles( ) {//
        Board board = FindObjectOfType<Board>( );//
        int boardDimensions = TileCoordinates.Length;//

        for (int i = 0; i < boardDimensions; i++) {
            GameObject tileprefab = Instantiate(Resources.Load(ResourcePath.boardTile), TileCoordinates[i], Quaternion.identity) as GameObject;
            Tile tile = tileprefab.GetComponent<Tile>( ) as Tile;
            tileprefab.transform.SetParent( board.transform );         
            TileTable.Add( TileCoordinates[i], tile as Tile );
            MoveTable.Add( TileCoordinates[i], true );
        }
    }
}
