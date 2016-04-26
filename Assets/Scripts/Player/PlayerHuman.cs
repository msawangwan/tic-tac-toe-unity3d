using UnityEngine;
using System;
using System.Collections;

public class PlayerHuman : Player, IPlayer {
    public bool IsTurnActive { get; private set; }

    protected override bool AttemptMove<T>( ) {
        if ( Input.GetMouseButtonDown ( 0 ) ) {
            if ( isTurn ) {
                T hitComponent = HitComponent<T>() as T;
                if ( hitComponent != null && hitComponent is Tile ) {
                    Tile selectedTile = hitComponent as Tile;
                    if ( selectedTile.isAValidMove == true ) {
                        IsTurnActive = turn.ExecuteTurn ( selectedTile );
                    }
                }
            }
        }
    }

    public void EnterTurn ( ) {
        IsTurnActive = true;
    }

    public void TakeTurn ( ) {
        AttemptMove<Tile> ( );
    }

    public void ExitTurn ( ) {
        IsTurnActive = false;
    }

    public event Action<PlayerTurnExitEvent> ExitTurnEvent;
    
    private void OnTurnEnd() {
        Logger.DebugToConsole ( "PlayerHuman" , "OnTurnEnd" , "Ending turn." );
        IPlayer nextPlayer = FindObjectOfType<PlayerComputer>();
        PlayerTurnExitEvent endTurnEvent = new PlayerTurnExitEvent( nextPlayer );
        ExitTurnEvent ( endTurnEvent );
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
