using UnityEngine;
using System.Collections;

public enum CellState {
    Empty = -1,
    X = 0,
    O = 1,
}

public class TicTacToeGame {
    private Grid2D grid;

    public TicTacToeGame(Grid2D grid) {
        this.grid = grid;
        BoardSetup ( );
    }

    private void BoardSetup() {
        int numCells = grid.Grid2DData.GridObject.transform.childCount;
        for ( int i = 0; i < numCells; i++ ) {
            TicTacToeCell newCell = grid.Grid2DData.GridObject.transform.GetChild ( i ).gameObject.AddComponent<TicTacToeCell> ( );
            newCell.InitialiseCell ( );
        }
    }
}
