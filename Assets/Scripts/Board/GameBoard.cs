using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//GameObject tilePrefab = MonoBehaviour.Instantiate<GameObject> (Resources.Load<GameObject> (ResourcePath.boardTile), tileCoordinates[i], Quaternion.identity);
//Tile tile = tilePrefab.GetComponent<Tile>( );
//tile.InitState ( );

public class GameBoard : IGameBoard, IFadeableGameObject {
    private GameObject boardObject;

    private Vector2[] tileCoordinates;

    public Dictionary<Vector2 , Tile> TileTable { get; private set; }
    public Vector2 BoardCenterPoint {
        get {
            int numCoordinates = tileCoordinates.Length;
            Vector2 sum = new Vector2(0,0);

            foreach ( Vector2 coord in tileCoordinates ) {
                sum += coord;
            }

            float centerX = sum.x;
            float centerY = sum.y;
            centerX = centerX / numCoordinates;
            centerY = centerY / numCoordinates;

            return new Vector2 ( centerX , centerY );
        }
    }

    public int BoardWidth { get; private set; }
    public int BoardHeight { get; private set; }

    public bool HasTiles { get; private set; }
    public bool IsDrawnToScreen { get; private set; }

    public GameBoard ( int width, int height ) {
        HasTiles = false;
        IsDrawnToScreen = false;

        boardObject = MonoBehaviour.Instantiate<GameObject> ( new GameObject() );
        boardObject.name = "Board";
        boardObject.AddComponent<BoardTicTacToe> ( );

        BoardWidth = width;
        BoardHeight = height;

        BoardContainer.AttachToTransformAsChild ( boardObject );        
    }

    public Vector2[] CreateTileWorldPositions ( ) {
        if ( tileCoordinates == null ) {

            tileCoordinates = new Vector2[BoardWidth * BoardHeight];

            for ( int i = 0, x = 0; x < BoardWidth; x++ ) {
                for ( int y = 0; y < BoardHeight; y++, i++ ) {
                    tileCoordinates[i] = new Vector2 ( x , y );
                }
            }
        }
        return tileCoordinates;
    }

    public GameObject CreateBoard ( Vector2[] coordinates ) {
        if ( boardObject ) {
            if ( IsDrawnToScreen == false && HasTiles == false) {
                GameObject tilePrefab = Resources.Load<GameObject> ( ResourcePath.boardTile );

                TileTable = new Dictionary<Vector2 , Tile> ( );
                TileTable.Clear ( );

                int boardDimensions = coordinates.Length;
                for ( int i = 0; i < boardDimensions; i++ ) {
                    MonoBehaviour.Instantiate ( tilePrefab , coordinates[i] , Quaternion.identity );

                    tilePrefab.SetActive ( false );
                    tilePrefab.transform.SetParent ( boardObject.transform );
                    tilePrefab.GetComponent<Tile> ( ).InitState ( );

                    TileTable.Add ( coordinates[i] , tilePrefab.GetComponent<Tile> ( ) );
                }
                HasTiles = true;
            }
            return boardObject;
        }
        return null;
    }

    public IEnumerable DrawTiles ( IFadeableGameObject fadingTiles, float fadeMultiplier ) {
        if ( IsDrawnToScreen == false && HasTiles ) {
            foreach ( Vector2 coord in tileCoordinates ) {
                if ( TileTable.ContainsKey ( coord ) ) {
                    Tile current = TileTable[coord];
                    current.GetComponent<IFadeableGameObject>().FadeIn ( .6f );
                    yield return new WaitForSeconds ( fadeMultiplier );
                }
            }
            IsDrawnToScreen = true;
        }
    }

    // implements 'IFadeAbleGameObject' -- fades each child tild of boarObject
    public IEnumerable FadeIn ( float fadeMultiplier ) { yield return null; }
    public IEnumerable FadeOut ( float fadeMultiplier ) {
        if ( IsDrawnToScreen ) {
            ITile[] boardTiles = boardObject.GetComponentsInChildren<ITile>();
            foreach ( IFadeableGameObject tile in boardTiles ) {
                yield return tile.FadeOut ( fadeMultiplier ).GetEnumerator ( );
                yield return new WaitForSeconds ( fadeMultiplier );
            }
            HasTiles = false;
        }
    }

    public void DestroyBoardGameObject ( ) {
        if (HasTiles == false) {
            MonoBehaviour.Destroy ( boardObject );
            IsDrawnToScreen = false;
        }
    }
}
