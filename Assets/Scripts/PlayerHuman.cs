using UnityEngine;
using System.Collections;

public class PlayerHuman : Player {
    protected override void Awake( ) {
        base.Awake( );
    }

    protected override void Update( ) {
        base.Update( );
    }

    protected override void MakeAMove<T>( ) {
        if (Input.GetMouseButtonDown( 0 )) {
            if (isTurn) {
                T hitComponent = HitComponent<T>() as T;
                if (hitComponent != null && hitComponent is Tile) {
                    Tile selectedTile = hitComponent as Tile;
                    gamemanager.MakeMove( selectedTile, playerID );
                }
            }
        }
    }

    // returns a selected component
    private T HitComponent<T>( ) where T : Component {
        RaycastHit2D hit = Physics2D.Raycast( Camera.main.ScreenToWorldPoint( Input.mousePosition ), Vector2.zero );
        if (Input.GetMouseButton( 0 )) {
            if (hit.transform == null) {
                return null;
            }
        }
        T hitComponent = hit.transform.GetComponent<T>();
        return hitComponent;
    }
}
