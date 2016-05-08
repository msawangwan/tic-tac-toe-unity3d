using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Currently, AI always has id of 0.
/// </summary>
public class PlayerComputer : Player, IPlayerMove {
    private Grid2D grid;
    private TicTacToeBoard currentGame;

    Dictionary<Vector2, GameObject> verticies;
    private Stack<TicTacToeMove> moveStack;

    private bool triggeredMoveSearch = false;

    public override void NewGameState ( ) {
        base.NewGameState ( );
        GetGridReferenceForAI ( );
        currentGame = new TicTacToeBoard (grid, 2 , 3 , 3 );
    }

    public override void EnterTurn ( ) {
        base.EnterTurn ( );
        triggeredMoveSearch = false;
    }

    protected override bool AttemptMove<T> ( ) {
        HasMadeValidMove = false;
        if (triggeredMoveSearch == false) {
            triggeredMoveSearch = true;
            IteratePossibleMoves ( );
        }
        return HasMadeValidMove;
    }

    /* Turn end event handler. */
    protected override PlayerTurnExitEvent MadeValidMove ( ) {
        Player opponentPlayer = FindObjectOfType<PlayerHuman>();
        IPlayer nextPlayer = opponentPlayer.GetComponent<IPlayer>();
        IPlayerTurn nextPlayerTurn = opponentPlayer.GetComponent<IPlayerTurn>();

        return new PlayerTurnExitEvent ( nextPlayer, nextPlayerTurn );
    }

    /* Find grid in the scene. */
    private void GetGridReferenceForAI ( ) {
        grid = FindObjectOfType<Grid2D> ( );
    }

    /* Naive move algorithm, simply finds the first empty cell as the next move. */
    private void NaiveMove() {
        foreach ( Transform tform in grid.Grid2DData.GridObject.transform ) {
            Vector2 cell = new Vector2(tform.position.x, tform.position.y);
            if ( tform.GetComponent<Grid2DInteractable> ( ).InteractionState ( ) ) {
                HasMadeValidMove = VerifyMove ( tform, Color.red, PlayerByID );
                break;
            }
        }
    }

    private void IteratePossibleMoves() {
        Transform node = null;
        verticies = grid.Grid2DData.VertexTable;
        moveStack = new Stack<TicTacToeMove>();

        foreach ( KeyValuePair<Vector2 , GameObject> v in verticies ) {
            if ( v.Value.GetComponent<TicTacToeCell> ( ).Mark == CellState.Empty ) {
                node = v.Value.transform;
                break;
            }
        }

        TicTacToeMove firstValidMove = new TicTacToeMove(new Vector2(node.transform.position.x, node.transform.position.y));
        TicTacToeMove bestMove = AlphaBeta(currentGame, firstValidMove, 0, 0);
        Vector2 movePosition = new Vector2(bestMove.Move.x, bestMove.Move.y);

        currentGame.AddMove (0,  bestMove );
        Transform move = grid.Grid2DData.VertexTable[movePosition].transform;

        while(moveStack.Count > 0) {
            grid.Grid2DData.VertexTable[moveStack.Pop ( ).Move].GetComponent<TicTacToeCell> ( ).Mark = CellState.Empty; 
        }

        HasMadeValidMove = VerifyMove ( move , Color.red , PlayerByID );     
    }

    private TicTacToeMove AlphaBeta (TicTacToeBoard board, TicTacToeMove move, int playerByID, int depthCount ) {
        board.AddMove ( playerByID, move );
        moveStack.Push ( move );
        if (board.IsGameOver) {
            move.Score = board.GetScore ( );          
            return move;
        }

        int nextPlayerByID = -1;

        if (playerByID == 0) {
            nextPlayerByID = 1;
        } else {
            nextPlayerByID = 0;
        }

        TicTacToeMove bestMove = null;

        foreach ( TicTacToeMove nextMove in board.PossibleMoves ( ) ) {
            TicTacToeBoard boardCopy = board;
            TicTacToeMove scoredMove = AlphaBeta(boardCopy, nextMove, nextPlayerByID, depthCount+1);
            if (bestMove == null || bestMove.Score < scoredMove.Score) {
                bestMove = scoredMove;
            }
        }

        board.RemoveMove ( move );
        return bestMove;
    }
}