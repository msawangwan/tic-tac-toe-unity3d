using UnityEngine;
using System.Collections;
/// <summary>
/// http://blog.circuitsofimagination.com/2014/06/29/MiniMax-and-Tic-Tac-Toe.html
/// https://www.ocf.berkeley.edu/~yosenl/extras/alphabeta/alphabeta.html
/// </summary>
public enum Cell {
    Empty = 0,
    X = 1,
    O = 2,
}

public class Grid2DTicTacToe {
    private Cell[][] board;
    private int gridSize;

    public Grid2DTicTacToe(int x, int y) {
        gridSize = x * y;
        board = new Cell[x][];
        

        for ( int i = 0; i < x; i++ ) {
            board[i] = new Cell[y];
            for ( int j = 0; j < y; j++ ) {
                board[i][j] = Cell.Empty;
            }
        }
    }

    public bool MakeMark(Cell playerMark, Vector2 cellPosition) {
        int currentX = (int) cellPosition.x;
        int currentY = (int) cellPosition.y;

        if ( currentX <= gridSize && currentY <= gridSize ) {
            if ( board[currentX][currentY] == Cell.Empty ) {
                board[currentX][currentY] = playerMark;
                return true;
            } 
        }
        return false;
    }

    public int Score(Game game) {
        if ( game.Winner ( ) == Cell.O ) return 1;
        else if ( game.Winner ( ) == Cell.X ) return -1;
        else
            return 0;
    }

    public int Maxi(Game game) {
        if (game.Over()) {
            return Score ( game );
        }

        int bestScore = -1;

        foreach(Cell cell in game.PossibleMoves()) {
            Game newGame = game.MakeCopy()
        }

        return bestScore;
    }

    public bool CheckWinner() {
        return false;
    }

    public Cell Winner() {
        return Cell.Empty;
    }
}

public class Game {
    int numCells;

    public Game() {

    }

    public Game(int numCells) {
        this.numCells = numCells;
    }

    public Cell Winner ( ) {
        return Cell.Empty;
    }

    public bool Over() {
        return false;
    }

    public Cell[] PossibleMoves() {
        Cell[] remainingMoves = new Cell[numCells];
        return remainingMoves;
    }

    public Game MakeCopy() {
        return new Game ( );
    }
}


