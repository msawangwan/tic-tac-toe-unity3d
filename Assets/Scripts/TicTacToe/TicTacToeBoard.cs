using UnityEngine;
using System.Collections.Generic;

public enum Marker {
    Blank = 0,
    X = 1,
    O = 2,
}

public class TicTacToeBoard {
    public Marker[][] Board { get; set; }

    public TicTacToeBoard ( ) { }

    public Marker[][] CreateEmptyBoard ( int numRows, int numCols ) {
        int r = numRows; // aka x
        int q = numCols; // aka y
        Marker[][] cells = new Marker[r][];

        for ( int i = 0; i < numRows; i++ ) {
            cells[i] = new Marker[numCols];
            for ( int j = 0; j < numCols; j++ ) {
                cells[i][j] = Marker.Blank;
            }
        }
        return cells;
    }

    public Marker[][] CopyBoard ( Marker[][] toCopy ) {
        Marker[][] copy = new Marker[toCopy.Length][];

        for ( int i = 0; i < toCopy.Length; i++ ) {
            copy[i] = new Marker[toCopy[i].Length];
            for ( int j = 0; j < toCopy[i].Length; j++ ) {
                copy[i][j] = toCopy[i][j];
            }
        }
        return copy;
    }

    public Marker[][] PlaceMarker ( Marker[][] board, Marker player, Vector2 cellPos ) {
        board[(int) cellPos.x][(int) cellPos.y] = player;
        return board;
    }

    public Marker GetOppositeMarker ( Marker player ) {
        if ( player == Marker.X ) {
            return Marker.O;
        } else {
            return Marker.X;
        }
    }

    public void RemoveMarker ( Marker[][] board, Vector2 cellPos ) {
        board[(int) cellPos.x][(int) cellPos.y] = Marker.Blank;
    }

    public List<Vector2> FindBlankCells ( Marker[][] board ) {
        List<Vector2> blankCells = new List<Vector2>();

        for ( int i = 0; i < board.Length; i++ ) {
            for ( int j = 0; j < board[i].Length; j++ ) {
                if ( board[i][j] == Marker.Blank ) {
                    Vector2 b = new Vector2 ( i, j );
                    blankCells.Add ( b );
                }
            }
        }
        return blankCells;
    }

    public bool CheckForWinner ( Marker[][] board, Marker player ) {
        /* Each cell of the board as a 2D-Point. */
        Vector2 cell0 = new Vector2(0,0);
        Vector2 cell1 = new Vector2(0,1);
        Vector2 cell2 = new Vector2(0,2);

        Vector2 cell3 = new Vector2(1,0);
        Vector2 cell4 = new Vector2(1,1);
        Vector2 cell5 = new Vector2(1,2);

        Vector2 cell6 = new Vector2(2,0);
        Vector2 cell7 = new Vector2(2,1);
        Vector2 cell8 = new Vector2(2,2);

                /* Check the three rows. */
        return (CheckLine ( board, player, cell0, cell1, cell2 ) ||
                CheckLine ( board, player, cell3, cell4, cell5 ) ||
                CheckLine ( board, player, cell6, cell7, cell8 ) ||
                /* Check the three columns. */
                CheckLine ( board, player, cell0, cell3, cell6 ) ||
                CheckLine ( board, player, cell1, cell4, cell7 ) ||
                CheckLine ( board, player, cell2, cell5, cell8 ) ||
                /* Check the two diagonals. */
                CheckLine ( board, player, cell0, cell4, cell8 ) ||
                CheckLine ( board, player, cell2, cell4, cell6 ));
    }

    public bool CheckForDraw ( Marker[][] board ) {
        for ( int i = 0; i < board.Length; i++ ) {
            for ( int j = 0; j < board[i].Length; j++ ) {
                if ( board[i][j] == Marker.Blank ) {
                    return false;
                }
            }
        }
        return true;
    }

    private bool CheckLine ( Marker[][] board, Marker player, Vector2 c1, Vector2 c2, Vector2 c3 ) {
        if ( board[(int) c1.x][(int) c1.y] == player &&
            board[(int) c2.x][(int) c2.y] == player &&
            board[(int) c3.x][(int) c3.y] == player ) {
            return true;
        }
        return false;
    }
}