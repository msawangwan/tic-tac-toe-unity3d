using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class Grid2DTile : MonoBehaviour, IFadeableGameObject {
    public float fadeTime { get; private set; }
    public bool IsUnmarked { get; private set; }

    private SpriteRenderer sprite;
    private Color defaultColor = Color.white;
    private Color alpha;

    private bool isDrawn;

    public void InitOnStart ( ) {
        gameObject.SetActive ( false );

        SetSprite ( );
        SetTransparencyZero ( );
        fadeTime = 25.5f;
    }

    public void MarkBycolor ( Color c ) {
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

    private void SetTransparencyZero ( ) {
        isDrawn = false;

        alpha = sprite.color;
        alpha.a = 0;
        sprite.color = new Color ( alpha.r , alpha.g , alpha.b , alpha.a );
    }

    private void SetSprite() {
        Sprite s = Resources.Load<Sprite> ( ResourcePath.grid2DTileBasic );

        sprite = GetComponent<SpriteRenderer> ( );
        sprite.sprite = s;

        IsUnmarked = true;
    }
}
