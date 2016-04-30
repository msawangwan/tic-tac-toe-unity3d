using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Grid2DTicTacToe : Grid2D, IFadeableGameObject {
    public Tile[] BoardTiles { get; private set; }

    protected IEnumerable DrawGrid ( ) {
        float drawTime = 0.43f;
        Dictionary<Vector2, Grid2DVertex > verticies = grid2D.VertexTable;
        if ( isDrawnToScreen == false ) {
            BoardTiles = new Tile[grid2D.VertexTable.Count];

            int count = 0;
            foreach ( KeyValuePair<Vector2 , Grid2DVertex> vert in verticies ) {
                Tile tile = vert.Value as Tile;
                StartCoroutine ( tile.FadeIn ( drawTime ).GetEnumerator ( ) );
                BoardTiles[count] = tile;
                ++count;
                yield return new WaitForSeconds ( drawTime );
            }
        }
    }

    public IEnumerable FadeIn ( float fadeMultiplier ) {
        yield return null;
    }

    public IEnumerable FadeOut ( float fadeMultiplier ) {
        if ( isDrawnToScreen ) {
            Tile[] boardTiles = GetComponentsInChildren<Tile>();
            foreach ( IFadeableGameObject tile in boardTiles ) {
                StartCoroutine ( tile.FadeOut ( fadeMultiplier ).GetEnumerator ( ) );
                yield return new WaitForSeconds ( fadeMultiplier );
            }
            isDrawnToScreen = false;
            Destroy ( grid2D.GridObject );
        }
    }
}
