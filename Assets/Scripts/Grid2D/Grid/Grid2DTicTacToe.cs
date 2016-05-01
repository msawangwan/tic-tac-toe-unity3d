using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Grid2DTicTacToe : MonoBehaviour, IFadeableGameObject {
    public float fadeTime { get; private set; }

    public void LayTilesOnGrid ( ) {
        foreach ( Transform v in transform ) {
            Grid2DTile t = v.gameObject.AddComponent<Grid2DTile> ( );
            Grid2DInteractable i = v.gameObject.AddComponent<Grid2DInteractable> ( );

            t.InitOnStart ( );
            i.InitOnStart ( );
        }
    }

    /* Can either yield an enumerator OR StartCoroutine, and in both cases, yield again. 
        Currently yielding WairForSeconds but try experimenting with other options. */
    public IEnumerable DrawTiles ( ) {
        foreach ( Transform v in transform ) {
            yield return v.GetComponent<Grid2DTile> ( ).FadeIn (  ).GetEnumerator ( );
            yield return new WaitForSeconds ( .08f );
        }
    }

    /* Implements IFadeableGameObject. 
    
        Fades tiles and deletes the grid GameObject once the last tile has an alpha of 0. */
    public IEnumerable FadeIn ( ) { yield return null; }
    public IEnumerable FadeOut ( ) {
        while (transform.childCount > 0) {
            foreach ( Transform v in transform ) {
                yield return v.GetComponent<Grid2DTile> ( ).FadeOut ( ).GetEnumerator ( );
                yield return new WaitForSeconds ( .47f );
            }
        }
        Destroy ( gameObject );
    }
}
