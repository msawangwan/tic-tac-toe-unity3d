using UnityEngine;

public class TicTacToeMove {
    public Vector2 Move { get; set; }
    public int Score { get; set; }

    public TicTacToeMove ( Vector2 move ) {
        Move = move;
        Score = 0;
    }

    public TicTacToeMove ( Vector2 move , int score ) {
        Move = move;
        Score = score;
    }
}
