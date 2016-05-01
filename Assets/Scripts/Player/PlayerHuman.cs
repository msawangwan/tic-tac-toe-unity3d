using UnityEngine;
using System;
using System.Collections;

public class PlayerHuman : Player, IPlayerMove {
    public bool HasMadeValidMove { get; private set; }

    protected override bool AttemptMove<T>( ) {
        HasMadeValidMove = false;
        if ( Input.GetMouseButtonDown ( 0 ) ) {
            T hitComponent = HitComponent<T>() as T;
            if ( hitComponent != null && hitComponent is GridInteractableObject ) {
                GridInteractableObject selected = hitComponent as GridInteractableObject;
                if ( selected.IsUnMarked ( ) ) {
                    HasMadeValidMove = VerifyMove( selected.transform, Color.blue );
                }
            }
        }
        return HasMadeValidMove;
    }

    // base class needs an instance of 'endTurnEvent'
    protected override PlayerTurnExitEvent MadeValidMove ( ) {
        Logger.DebugToConsole ( "PlayerHuman", "MadeValidMove", "Ending turn." );
        Player opponentPlayer = FindObjectOfType<PlayerComputer>();
        IPlayer nextPlayer = opponentPlayer.GetComponent<IPlayer>();
        IPlayerTurn nextPlayerTurn = opponentPlayer.GetComponent<IPlayerTurn>();

        return new PlayerTurnExitEvent( nextPlayer, nextPlayerTurn );
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
