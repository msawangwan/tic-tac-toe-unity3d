using UnityEngine;
using System.Collections;

public class TicTacToeCell : MonoBehaviour {
    public CellState Mark { get; set; }

    public void InitialiseCell ( ) {
        Mark = CellState.Empty;
    }

    public void MarkCell ( int playerByID ) {
        if (playerByID == 0) {
            Mark = CellState.X;
        } else {
            Mark = CellState.O;
        }
    }
}
