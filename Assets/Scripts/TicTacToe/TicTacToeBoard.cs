using UnityEngine;
using System.Collections.Generic;

public class TicTacToeBoard {
    public bool IsGameOver { get; private set; }

    private Grid2D grid;

    private int rowSize;
    private int colSize;
    private int winner = -1;

    public TicTacToeBoard( Grid2D grid, int numPlayers, int numRows, int numCols) {
        this.grid = grid;
        IsGameOver = false;

        rowSize = numRows;
        colSize = numCols;
    }

    public void AddMove ( int playerID , TicTacToeMove move ) {
        Vector2 moveToAdd = move.Move;
        grid.Grid2DData.VertexTable[moveToAdd].transform.gameObject.GetComponent<TicTacToeCell> ( ).MarkCell ( playerID );
        IsGameOver = CheckForWinCondition ( );
    }

    public void RemoveMove ( TicTacToeMove move ) {
        Vector2 moveToRemove = move.Move;
        grid.Grid2DData.VertexTable[moveToRemove].transform.gameObject.GetComponent<TicTacToeCell> ( ).InitialiseCell ( );
        Debug.Log ( "REMOVING " + grid.Grid2DData.VertexTable[moveToRemove].transform.gameObject.GetComponent<TicTacToeCell> ( ).Mark );
    }

    public List<TicTacToeMove> PossibleMoves ( ) {
        List<TicTacToeMove> moves = new List<TicTacToeMove>();

        int numMoves = grid.Grid2DData.GridObject.transform.childCount;
        for ( int i = 0; i < numMoves; i++ ) {
            if ( grid.Grid2DData.GridObject.transform.GetChild ( i ).GetComponent<TicTacToeCell> ( ).Mark == CellState.Empty ) {
                int x = (int) grid.Grid2DData.GridObject.transform.GetChild(i).transform.position.x;
                int y = (int) grid.Grid2DData.GridObject.transform.GetChild(i).transform.position.y;

                Vector2 possibleMove = new Vector2(x, y);
                moves.Add ( new TicTacToeMove ( possibleMove ) );
            }
        }

        return moves;
    }

    public int GetScore() {
        if ( IsGameOver ) {
            if ( winner == 0 )
                return 10;
            else if ( winner == 1 )
                return -10;
        }
        return 0;
    }

    private bool CheckForWinCondition() {
        int numCells = grid.Grid2DData.GridObject.transform.childCount;
        int numMovesMade = 0;

        int[] cols_X = new int[colSize];
        int[] rows_X = new int[rowSize];
        int[] cols_O = new int[colSize];
        int[] rows_O = new int[rowSize];

        int diag0_X = 0;
        int diag1_X = 0;
        int diag0_O = 0;
        int diag1_O = 0;

        bool isGameOver = false;

        for ( int i = 0; i < numCells; i++ ) {
            Transform node = grid.Grid2DData.GridObject.transform.GetChild ( i );
            CellState cell = node.GetComponent<TicTacToeCell> ( ).Mark;

            if (cell == CellState.Empty)
                continue;

            ++numMovesMade;
            if ( numMovesMade >= ( colSize * rowSize ) ) {
                winner = -1;
                isGameOver = true;
                return true;
            }
                
            int col = (int) node.position.x;
            int row = (int) node.position.y;

            if (cell == CellState.X) {
                ++cols_X[col];
                ++rows_X[row];
                isGameOver = CheckHorizontalAndVertical ( cols_X , col , rows_X , row );

                if ( !isGameOver ) {
                    if ( col == row )
                        ++diag0_X;
                    if ( row == ( colSize - 1 ) - col )
                        ++diag1_X;
                    isGameOver = CheckDiaganols ( diag0_X , diag1_X );
                }

                if ( isGameOver ) {
                    winner = 0;
                    return true;
                }

            } else if (cell == CellState.O) {
                ++cols_O[col];
                ++rows_O[row];
                isGameOver = CheckHorizontalAndVertical ( cols_O , col , rows_O , row );

                if ( !isGameOver ) {
                    if ( col == row )
                        ++diag0_O;
                    if ( row == ( colSize - 1 ) - col )
                        ++diag1_O;
                    isGameOver = CheckDiaganols ( diag0_O , diag1_O );
                }

                if ( isGameOver ) {
                    winner = 1;
                    return true;
                }
            }
        }
        return false;
    }

    private bool CheckHorizontalAndVertical(int[] cols, int col, int[] rows, int row) {
        if ( cols[col] >= colSize || rows[row] >= rowSize )
            return true;
        return false;
    }

    private bool CheckDiaganols(int diag0, int diag1) {
        if ( diag0 >= colSize || diag1 >= colSize )
            return true;
        return false;
    }
}