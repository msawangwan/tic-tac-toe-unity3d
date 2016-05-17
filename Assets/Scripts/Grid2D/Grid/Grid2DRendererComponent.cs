using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Grid2DRendererComponent : MonoBehaviour, IFadeableGameObject {
    public float fadeTime { get; private set; }

    private AudioSource sfxplayer;
    private AudioClip[] sfx;

    /* Attaches Tile and Interactable components to each vertex of the grid. */
    public void LayTilesOnGrid ( ) {
        foreach ( Transform v in transform ) {
            Grid2DVertexRendererComponent t = v.gameObject.AddComponent<Grid2DVertexRendererComponent> ( );

            t.InitOnStart ( );
        }
    }

    /* Call this method only after LayTilesOnGrid has been called. */
    public IEnumerable DrawTiles ( ) {
        
        foreach ( Transform v in transform ) {
            int rand = UnityEngine.Random.Range(0, sfx.Length);
            sfxplayer.PlayOneShot ( sfx[rand] );
            sfxplayer.pitch += .1f;
            yield return v.GetComponent<Grid2DVertexRendererComponent> ( ).FadeIn (  ).GetEnumerator ( );
            yield return new WaitForSeconds ( .08f );
        }
    }

    /* Implements IFadeableGameObject. */
    public IEnumerable FadeIn ( ) { yield return null; }
    public IEnumerable FadeOut ( ) {
        while (transform.childCount > 0) {
            foreach ( Transform v in transform ) {
                int rand = UnityEngine.Random.Range(0, sfx.Length);
                sfxplayer.PlayOneShot ( sfx[rand] );
                sfxplayer.pitch -= .2f;
                yield return v.GetComponent<Grid2DVertexRendererComponent> ( ).FadeOut ( ).GetEnumerator ( );
                yield return new WaitForSeconds ( .47f );
            }
        }
        Destroy ( gameObject );
    }

    private void Start() {
        sfxplayer = gameObject.AddComponent<AudioSource> ( );
        sfxplayer.volume = .25f;

        sfx = SFXMasterController.LoadBloopSFX();
    }
}
