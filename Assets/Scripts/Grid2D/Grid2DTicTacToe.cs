using UnityEngine;
using System.Collections.Generic;
/// <summary>
/// http://blog.circuitsofimagination.com/2014/06/29/MiniMax-and-Tic-Tac-Toe.html
/// https://www.ocf.berkeley.edu/~yosenl/extras/alphabeta/alphabeta.html
/// </summary>
public enum CellMarking {
    Empty = 0,
    X = 1,
    O = 2,
}

public class Grid2DTicTacToe {
    private CellMarking[][] board;
    private int gridSize;

    public Grid2DTicTacToe(int x, int y) {
        gridSize = x * y;
        board = new CellMarking[x][];
        

        for ( int i = 0; i < x; i++ ) {
            board[i] = new CellMarking[y];
            for ( int j = 0; j < y; j++ ) {
                board[i][j] = CellMarking.Empty;
            }
        }
    }

    public int Score(Game game) {
        if ( game.Winner ( ) == CellMarking.O ) return 1;
        else if ( game.Winner ( ) == CellMarking.X ) return -1;
        else
            return 0;
    }

    public int Maxi(Game game) {
        if (game.Over()) {
            return Score ( game );
        }

        int bestScore = -1;

        foreach( Vector2 cell in game.PossibleMoves() ) {
            Game newGame = game.MakeCopy(board);
            newGame.MakeMove ( CellMarking.X, cell );
            int score = Mini(newGame);

            if ( score > bestScore ) {
                bestScore = score;
            }
        }

        return bestScore;
    }

    public int Mini(Game game) {
        if ( game.Over ( ) ) {
            return Score ( game );
        }

        int worstScore = 1;

        foreach ( Vector2 cell in game.PossibleMoves ( ) ) {
            Game newGame = game.MakeCopy(board);
            newGame.MakeMove ( CellMarking.X, cell );
            int score = Maxi(newGame);

            if ( score < worstScore ) {
                worstScore = score;
            }
        }
        return worstScore;
    }
}

public class Game {
    bool isOver = false;
    CellMarking winner = CellMarking.Empty;
    CellMarking[][] board;

    public Game(int x, int y) {
        int gridSize = x * y;
        board = new CellMarking[x][];


        for ( int i = 0; i < x; i++ ) {
            board[i] = new CellMarking[y];
            for ( int j = 0; j < y; j++ ) {
                board[i][j] = CellMarking.Empty;
            }
        }
    }

    public Game(CellMarking[][] board) {
        this.board = board;
    }

    public CellMarking Winner ( ) {
        return winner;
    }

    public bool Over() {
        return isOver;
    }

    /* Returns a list of cells marked Empty. */
    public List<Vector2> PossibleMoves () {
        List<Vector2> possibleMoves = new List<Vector2>();

        for ( int i = 0; i < board.Length; i++ ) {
            for ( int j = 0; j < board[i].Length; j++ ) {
                if (board[i][j] == CellMarking.Empty) {
                    Vector2 move = new Vector2(i, j);
                    possibleMoves.Add ( move );
                }
            }
        }

        return possibleMoves;
    }

    /* Returns a Game object that uses a copied board. */
    public Game MakeCopy(CellMarking[][] board) {
        return new Game ( board );
    }

    /* Marks a given cell as an X or O. */
    public void MakeMove(CellMarking player, Vector2 move) {
        int x = (int) move.x;
        int y = (int) move.y;

        if (board[x][y] == CellMarking.Empty)
            board[x][y] = player;
    }

    public bool CheckBoard() {
        int[] row_X = new int[board.Length];
        int[] col_X = new int[board[0].Length];
        int diag0_X = 0;
        int diag1_X = 0;

        int[] row_O = new int[board.Length];
        int[] col_O = new int[board[0].Length];
        int diag0_O = 0;
        int diag1_O = 0;

        int moveCount = 0;

        for (int i = 0; i < board.Length; i++ ) {
            for(int j = 0; j < board[i].Length; j++ ) {
                if ( board[i][j] == CellMarking.X ) {
                    ++moveCount;
                    ++row_X[i];
                    ++col_X[j];

                    if ( col_X[j] >= board.Length || row_X[i] >= board[0].Length ) {
                        isOver = true;
                        winner = CellMarking.X;
                        break;
                    }
                    if ( i == j ) {
                        ++diag0_X;
                    }
                    if ( diag0_X >= board.Length ) {
                        isOver = true;
                        winner = CellMarking.X;
                        break;
                    }
                    if ( i == (board.Length - 1) - j ) {
                        ++diag1_X;
                    }
                    if ( diag1_X >= board.Length ) {
                        isOver = true;
                        winner = CellMarking.X;
                        break;
                    }

                } else if ( board[i][j] == CellMarking.O ) {
                    ++moveCount;
                    ++row_O[i];
                    ++col_O[j];

                    if ( col_O[j] >= board.Length || row_O[i] >= board[0].Length ) {
                        isOver = true;
                        winner = CellMarking.O;
                        break;
                    }
                    if ( i == j ) {
                        ++diag0_O;
                    }
                    if ( diag0_O >= board.Length ) {
                        isOver = true;
                        winner = CellMarking.O;
                        break;
                    }
                    if ( i == (board.Length - 1) - j ) {
                        ++diag1_O;
                    }
                    if ( diag1_O >= board.Length ) {
                        isOver = true;
                        winner = CellMarking.O;
                        break;
                    }

                }
            }
        }

        if ( moveCount >= board.Length * board[0].Length ) {
            isOver = true;
        }

        return isOver;
    }
}


