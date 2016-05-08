using UnityEngine;
using System.Collections;

public class TicTacToeCell : MonoBehaviour {
    public CellState Mark { get; private set; }

    public void InitialiseCell ( ) {
        Mark = CellState.Empty;
        //Debug.Log ( "UNMARKING " + transform.position );
    }

    public void MarkCell ( int playerByID ) {
        if (playerByID == 0) {
            Mark = CellState.X;
        } else {
            Mark = CellState.O;
        }
        //Debug.Log ( "Player Marked at position: " + transform.position + " , " + playerByID );
    }
}
