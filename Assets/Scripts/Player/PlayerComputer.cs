using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Currently, AI always has id of 0.
/// </summary>
public class PlayerComputer : Player {
    private Grid2DComponent grid;

    public override void NewGameState ( ) {
        base.NewGameState ( );
        GetGridReferenceForAI ( );
    }

    public TicTacToeMove NegaMaxMove ( TicTacToeBoard game ) {
        List<Vector2> moves = game.FindBlankCells(game.Board);

        float score;

        TicTacToeMove bestMove = new TicTacToeMove();
        TicTacToeBoard boardCopy = new TicTacToeBoard();

        bestMove.Score = -10000;

        int depth = 0;

        foreach ( Vector2 v in moves ) {
            boardCopy.Board = game.CopyBoard ( game.Board );
            boardCopy.PlaceMarker ( boardCopy.Board, Marker.X, v );
            score = NegaMax ( boardCopy, boardCopy.GetOppositeMarker ( Marker.X ), depth );
            if ( score >= bestMove.Score ) {
                bestMove.Score = (int) score;
                bestMove.Cell = v;
            }
        }
        return bestMove;
    }

    public int NegaMax ( TicTacToeBoard game, Marker player, int depth ) {
        int maxScore = -1;
        int sign = 1;

        if ( game.CheckForWinner ( game.Board, Marker.X ) ) { // return score/move
            return StaticEvaluation ( Marker.X );
        } else if ( game.CheckForWinner ( game.Board, Marker.O ) ) {
            return StaticEvaluation ( Marker.O );
        } else if ( game.CheckForDraw ( game.Board ) ) { //return score/move
            return StaticEvaluation ( Marker.Blank );
        }

        if ( player != Marker.X ) {
            sign = -1;
        }

        List<Vector2> moves = game.FindBlankCells(game.Board);
        TicTacToeBoard gameCopy = new TicTacToeBoard();
        int score = 0;

        foreach ( Vector2 nextMove in moves ) {
            gameCopy.Board = gameCopy.CopyBoard ( game.Board );
            gameCopy.PlaceMarker ( gameCopy.Board, player, nextMove );
            score = sign * NegaMax ( gameCopy, gameCopy.GetOppositeMarker ( player ), depth + 1 );
            if ( score > maxScore ) {
                maxScore = score;
            }
        }
        return maxScore * sign;
    }

    private int StaticEvaluation ( Marker player ) {
        if ( player == Marker.X ) {
            return 1;
        } else if ( player == Marker.O ) {
            return -1;
        }
        return 0;
    }

    /* Find grid in the scene. */
    private void GetGridReferenceForAI ( ) {
        grid = FindObjectOfType<Grid2DComponent> ( );
    }
}