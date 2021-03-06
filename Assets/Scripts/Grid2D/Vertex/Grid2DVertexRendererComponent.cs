﻿using UnityEngine;
using System.Collections;
/// <summary>
/// Enables the capability to fade parent gameobjects alpha.
/// </summary>
[RequireComponent(typeof(SpriteRenderer))]
public class Grid2DVertexRendererComponent : MonoBehaviour, IFadeableGameObject {
    public float fadeTime { get; private set; }

    private SpriteRenderer sprite;
    private Color alpha;

    private bool isDrawn;

    /* Call on Attaching component to a GameObject. */
    public void InitOnStart ( ) {
        gameObject.SetActive ( false );

        SetSprite ( );
        SetTransparencyZero ( );
        fadeTime = 25.5f;
    }

    /* Sets tile color to players color. */
    public void UpdateColor ( Color c ) {
        sprite.color = c;
    }

    /* IFadeableGameObject */
    public IEnumerable FadeIn ( ) {
        if ( isDrawn == false ) {
            while ( sprite.color.a < 1 ) {
                gameObject.SetActive ( true );
                alpha = sprite.color;
                alpha.a += Time.deltaTime * fadeTime;
                sprite.color = new Color ( alpha.r , alpha.g , alpha.b , alpha.a );
                yield return null;
            }
            isDrawn = true;
        }
    }

    /* IFadeableGameObject */
    public IEnumerable FadeOut( ) {
        if (isDrawn == true) {
            while ( sprite.color.a > 0 ) {
                alpha = sprite.color;
                alpha.a -= Time.deltaTime * fadeTime;
                sprite.color = new Color ( alpha.r , alpha.g , alpha.b , alpha.a );
                yield return null;
            }
            isDrawn = false;
        }
        Destroy ( gameObject );
    }

    /* Sets the sprite alpha to 0. */
    private void SetTransparencyZero ( ) {
        isDrawn = false;

        alpha = sprite.color;
        alpha.a = 0;
        sprite.color = new Color ( alpha.r , alpha.g , alpha.b , alpha.a );
    }

    /* Attaches a sprite to the GameObject. */
    private void SetSprite() {
        Sprite s = Resources.Load<Sprite> ( ResourcePath.grid2DTileBasic );

        sprite = GetComponent<SpriteRenderer> ( );
        sprite.sprite = s;
    }
}
