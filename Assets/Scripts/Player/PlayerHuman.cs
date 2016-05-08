using UnityEngine;
using System;
using System.Collections;

public class PlayerHuman : Player, IPlayerMove {
    protected override bool AttemptMove<T>( ) {
        HasMadeValidMove = false;
        if ( Input.GetMouseButtonDown ( 0 ) ) {
            T hitComponent = HitComponent<T>() as T;
            if ( hitComponent != null && hitComponent is Grid2DInteractable ) {
                Grid2DInteractable selected = hitComponent as Grid2DInteractable;
                if ( selected.InteractionState ( ) ) {
                    HasMadeValidMove = VerifyMove( selected.transform, Color.blue, PlayerByID );
                }
            }
        }
        return HasMadeValidMove;
    }

    /* Base class needs an instance of PlayerTurnExitEvent. */
    protected override PlayerTurnExitEvent MadeValidMove ( ) {
        Player opponentPlayer = FindObjectOfType<PlayerComputer>();
        IPlayer nextPlayer = opponentPlayer.GetComponent<IPlayer>();
        IPlayerTurn nextPlayerTurn = opponentPlayer.GetComponent<IPlayerTurn>();

        return new PlayerTurnExitEvent( nextPlayer, nextPlayerTurn );
    }

    /* Returns a gameobject and components at player click position. */
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
