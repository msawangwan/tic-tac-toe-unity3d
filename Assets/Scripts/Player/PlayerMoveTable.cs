using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMoveTable {   
    public const int RowSize = 3;
    public const int ColSize = 3;

    public int[] Rows { get; private set; }
    public int[] Cols { get; private set; }
    public int Diag0 { get; private set; }
    public int Diag1 { get; private set; }

    private bool playerWon = false;

    public PlayerMoveTable() {
        //Rows = new int[RowSize * ColSize]; <- possible to delete if nothing breaks
        //Cols = new int[RowSize * ColSize]; <- ditto

        Rows = new int[RowSize];
        Cols = new int[ColSize];
    }

    public void IncrementMove(Vector2 move) {
        int moveX = (int) move.x;
        int moveY = (int) move.y;

        ++Cols[moveX];
        ++Rows[moveY];
        if (Cols[moveX] >= ColSize) {
            playerWon = true;
        }
                  
        if (Rows[moveY] >= RowSize) {
            playerWon = true;
        }
            
        if (moveX == moveY)
            ++Diag0;
        if (Diag0 >= ColSize)
            playerWon = true;

        if (moveY == (ColSize - 1) - moveX)
            ++Diag1;
        if (Diag1 >= ColSize)
            playerWon = true;
    }

    public bool CheckForTicTacToe() {
        return playerWon;
    }
}
