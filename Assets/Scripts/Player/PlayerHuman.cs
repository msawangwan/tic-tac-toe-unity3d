using UnityEngine;
using System;
using System.Collections;

public class PlayerHuman : Player {
    /* Handles click input, returns the clicked GameObject based on component. */
    public GameObject ClickHandler<T> ( ) where T : Component {
        if ( Input.GetMouseButtonDown ( 0 ) ) {
            T hitComponent = HitComponent<T>() as T;
            if ( hitComponent != null && hitComponent is Grid2DInteractable ) {
                Debug.Log ( "Click ... " );
                return hitComponent.gameObject;
            }
        }
        return null;
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