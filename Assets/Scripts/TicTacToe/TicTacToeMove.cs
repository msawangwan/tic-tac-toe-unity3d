using UnityEngine;

public class TicTacToeMove {
    public Vector2 Cell { get; set; }
    public int Score { get; set; }

    public TicTacToeMove() {

    }

    public TicTacToeMove ( Vector2 move ) {
        Cell = move;
        Score = 0;
    }

    public TicTacToeMove ( Vector2 move , int score ) {
        Cell = move;
        Score = score;
    }
}
