using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour, ITile {
    private SpriteRenderer spriteRenderer;
    private Vector2 tilePosition;
    private Color defaultColor = Color.white;
    private bool isAValidMove = true;

    private void Start() {
        Initialise( );
    }

    private void Initialise() {
        spriteRenderer = GetComponent<SpriteRenderer>( );
        tilePosition = new Vector2(transform.position.x, transform.position.y);

        InitialiseTileState( );
    }

    public void InitialiseTileState() {
        spriteRenderer.color = defaultColor;
        isAValidMove = true;
    }

    public void MarkTileAsSelected(PlayerID player) {
        Color playerColor;
        if(player == 0) {
            playerColor = Color.red;
        } else {
            playerColor = Color.blue;
        }

        if (isAValidMove) {
            spriteRenderer.color = playerColor;
            isAValidMove = false;
        }
    }

    public Vector2 ReturnTilePosition() {
        return tilePosition;
    }
}
