using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class Grid2DTile : MonoBehaviour, IFadeableGameObject {
    public bool IsUnmarked { get; private set; }

    private SpriteRenderer sprite;
    private Color defaultColor = Color.white;
    private Color alpha;

    private bool isDrawn;

    public void InitOnStart ( ) {
        gameObject.SetActive ( false );
        sprite = GetComponent<SpriteRenderer> ( );
        SetSprite ( );
        SetTransparencyZero ( );
    }

    public void MarkBycolor ( Color c ) {
        sprite.color = c;
    }

    /* IFadeableGameObject */
    public IEnumerable FadeIn ( float fadeMultiplier ) {
        if ( isDrawn == false ) {
            while ( sprite.color.a < 1 ) {
                gameObject.SetActive ( true );
                alpha = sprite.color;
                alpha.a += Time.deltaTime * fadeMultiplier;
                sprite.color = new Color ( alpha.r , alpha.g , alpha.b , alpha.a );
                yield return null;
            }
            isDrawn = true;
        }
    }

    /* IFadeableGameObject */
    public IEnumerable FadeOut( float fadeMultiplier ) {
        if (isDrawn == true) {
            while ( sprite.color.a > 0 ) {
                alpha = sprite.color;
                alpha.a -= Time.deltaTime * fadeMultiplier;
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
        sprite.sprite = s;

        IsUnmarked = true;
    }
}
