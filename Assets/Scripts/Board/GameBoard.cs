using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// Class representing a 2D gameboard.
/// </summary>
public class GameBoard : MonoBehaviour {
    //private GameSettings settings;
    private Dictionary<Vector2, Tile> tileTable;
    //private Dictionary<Vector2, bool> legalMoveTable; // <-- might not need

    public bool isDrawn { get; private set; }

    public void Init( GameSettings gamesettings ) {
        //settings = gamesettings;
        //Assert.IsFalse ( settings == null );
    }

    public void DrawBoard( Vector2[] coordinates ) {
        if (isDrawn == false) {
            Vector2[] tileCoordinates = coordinates;
            int boardDimensions = tileCoordinates.Length;

            for ( int i = 0; i < boardDimensions; i++ ) {
                GameObject tilePrefab = Instantiate(Resources.Load(ResourcePath.boardTile), tileCoordinates[i], Quaternion.identity) as GameObject;
                Tile tile = tilePrefab.GetComponent<Tile>( ) as Tile;

                tilePrefab.transform.SetParent ( gameObject.transform );
                tileTable.Add ( tileCoordinates[i] , tile as Tile );
                //legalMoveTable.Add ( tileCoordinates[i] , true ); // <-- might not need
            }

            isDrawn = true;
        }
    }
}
