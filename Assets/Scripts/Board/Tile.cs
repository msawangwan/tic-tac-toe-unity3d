using UnityEngine;
using System.Collections;

public class Tile : Grid2DVertex, ITile, IFadeableGameObject {
    private SpriteRenderer spriteRenderer;
    private Color defaultColor = Color.white;

    public Vector2 Position { get; private set; }
    public bool IsAValidMove { get; private set; }

    public override void InitAsNew ( ) {
        base.InitAsNew ( );

        spriteRenderer = GetComponent<SpriteRenderer> ( );
        ResetSpriteRenderer ( );

        Position = vertexPos;
        IsAValidMove = true;
    }

    public void MarkTileAsSelected ( int playerByID ) {
        Color playerColor;
        if ( playerByID == 0 ) {
            playerColor = Color.red;
        } else {
            playerColor = Color.blue;
        }

        if ( IsAValidMove ) {
            spriteRenderer.color = playerColor;
            IsAValidMove = false;
        }
    }

    // implements 'IFadeAbleGameObject' -- fades each child tild of boarObject
    public IEnumerable FadeIn ( float fadeMultiplier ) {
        yield return null;
    }

    public IEnumerable FadeOut( float fadeMultiplier ) { // a good speed is between .3f and 5.0f
        while ( spriteRenderer.color.a > 0 ) {
            Color spriteAlpha = spriteRenderer.color;
            print ( "ALPHA" + spriteAlpha.a );
            spriteAlpha.a -= Time.deltaTime * fadeMultiplier;
            spriteRenderer.color = new Color(spriteAlpha.r, spriteAlpha.g, spriteAlpha.b, spriteAlpha.a );
            yield return null;
        }
        Destroy ( gameObject );
    }

    private void ResetSpriteRenderer() {
        spriteRenderer.color = defaultColor;
        Color spriteZeroAlpha = spriteRenderer.color;
        spriteZeroAlpha.a = 0;
        spriteRenderer.color = new Color ( defaultColor.r , defaultColor.g , defaultColor.b , spriteZeroAlpha.a );
    }
}
