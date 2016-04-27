using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour, ITile {
    private SpriteRenderer spriteRenderer;
    private Vector2 tilePosition;
    private Color defaultColor = Color.white;

    public bool isAValidMove { get; private set; }

    public void ResetTileState ( ) {
        tilePosition = new Vector2 ( transform.position.x , transform.position.y );
        spriteRenderer = GetComponent<SpriteRenderer> ( );
        spriteRenderer.color = defaultColor;
        isAValidMove = true;
    }

    public void MarkTileAsSelected ( int playerByID ) {
        Color playerColor;
        if ( playerByID == 0 ) {
            playerColor = Color.red;
        } else {
            playerColor = Color.blue;
        }

        if ( isAValidMove ) {
            spriteRenderer.color = playerColor;
            isAValidMove = false;
        }
    }

    public Vector2 ReturnTilePosition ( ) {
        return tilePosition;
    }

    // TODO: test and see if we can delete the start method (can just call the public 'constructor' when the tile is instantiated')
    private void Start() {
        tilePosition = new Vector2 ( transform.position.x , transform.position.y );
        spriteRenderer = GetComponent<SpriteRenderer> ( );
        spriteRenderer.color = defaultColor;       
        isAValidMove = true;
    }

}
