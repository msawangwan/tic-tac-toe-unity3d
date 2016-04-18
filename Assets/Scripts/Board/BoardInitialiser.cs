using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardInitialiser {
    private Vector2[] gameboardTileCoordinates;

    private Dictionary<Vector2, Tile> tileTable;

    public int GridWidth { get; private set; }
    public int GridHeight { get; private set; }

    public bool isDrawn { get; private set; }

    public Vector2 GameboardCenterPoint {
        get {
            int numCoordinates = gameboardTileCoordinates.Length;
            Vector2 sum = new Vector2(0,0);

            foreach ( Vector2 coor in gameboardTileCoordinates ) {
                sum += coor;
            }

            float centerX = sum.x;
            float centerY = sum.y;
            centerX = centerX / numCoordinates;
            centerY = centerY / numCoordinates;

            return new Vector2 ( centerX , centerY );
        }
    }

    public BoardInitialiser ( ) {
        isDrawn = false;
    }

    public BoardInitialiser ( int xDimension , int yDimension ) {
        isDrawn = false;
        GridWidth = xDimension;
        GridHeight = yDimension;
    }

    public Vector2[] GetGridCoordinates ( ) {
        if ( gameboardTileCoordinates == null ) {
            gameboardTileCoordinates = new Vector2[GridWidth * GridHeight];
            for ( int i = 0, x = 0; x < GridWidth; x++ ) {
                for ( int y = 0; y < GridHeight; y++, i++ ) {
                    gameboardTileCoordinates[i] = new Vector2 ( x , y );
                }
            }
        }

        return gameboardTileCoordinates;
    }

    public Dictionary<Vector2 , Tile> DrawBoard ( GameObject boardObject, Vector2[] coordinates ) {
        if ( isDrawn == false ) {
            Vector2[] tileCoordinates = coordinates;
            int boardDimensions = tileCoordinates.Length;

            tileTable = new Dictionary<Vector2 , Tile> ( );
            tileTable.Clear ( );

            for ( int i = 0; i < boardDimensions; i++ ) {
                GameObject tilePrefab = MonoBehaviour.Instantiate(Resources.Load(ResourcePath.boardTile), tileCoordinates[i], Quaternion.identity) as GameObject;
                Tile tile = tilePrefab.GetComponent<Tile>( ) as Tile;

                tilePrefab.transform.SetParent ( boardObject.transform );
                tileTable.Add ( tileCoordinates[i] , tile as Tile );
                //legalMoveTable.Add ( tileCoordinates[i] , true ); // <-- might not need
            }
            isDrawn = true;
        }

        return tileTable;
    }
}
