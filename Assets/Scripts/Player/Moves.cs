using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Moves : MonoBehaviour {   
    const int rowSize = 3;
    const int colSize = 3;
    int[] rows;
    int[] cols;
    int diag0;
    int diag1;
    bool playerWon = false;

    private void Start() {
        rows = new int[rowSize * colSize];
        cols = new int[rowSize * colSize];
    }

    public void IncrementMove(Vector2 move) {
        int moveX = (int) move.x;
        int moveY = (int) move.y;

        ++cols[moveX];
        ++rows[moveY];
        if (cols[moveX] >= colSize) {
            playerWon = true;
        }
                  
        if (rows[moveY] >= rowSize) {
            playerWon = true;
        }
            
        if (moveX == moveY)
            ++diag0;
        if (diag0 >= colSize)
            playerWon = true;

        if (moveY == (colSize - 1) - moveX)
            ++diag1;
        if (diag1 >= colSize)
            playerWon = true;
    }

    public bool CheckForThree() {
        return playerWon;
    }
}
