using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Grid2DTicTacToe : MonoBehaviour {
    public IEnumerable DrawTiles( float drawTime ) {
        foreach ( Transform v in transform ) {
            StartCoroutine ( v.GetComponent<Grid2DTile> ( ).FadeIn ( drawTime ).GetEnumerator ( ) );
            yield return new WaitForSeconds ( drawTime );
        }
    }

    public void LayTilesOnGrid ( ) {
        foreach ( Transform v in transform ) {
            Grid2DTile t = v.gameObject.AddComponent<Grid2DTile> ( );
            GridInteractableObject i = v.gameObject.AddComponent<GridInteractableObject> ( );

            t.InitOnStart ( );
            i.InitOnStart ( );
        }
    }
}
