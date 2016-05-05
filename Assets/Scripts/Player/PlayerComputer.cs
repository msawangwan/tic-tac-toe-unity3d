using UnityEngine;
using System.Collections.Generic;

public class PlayerComputer : Player, IPlayerMove {
    public bool HasMadeValidMove { get; private set; }

    private Grid2D grid;
    private int[,] board;
    private Pathfinder pathfinder;
    private Dictionary<Grid2DNode, Transform> movePriority;

    public override void NewGameState ( ) {
        base.NewGameState ( );
        GetGridReferenceForAI ( );
        board = new int[3 , 3];
        ZeroBoard ( );
        //MinMax ( );
        //pathfinder = new Pathfinder ( grid );
    }

    protected override bool AttemptMove<T>() {
        HasMadeValidMove = false;
        foreach ( Transform tform in grid.Grid2DData.GridObject.transform ) {         
            if ( tform.GetComponent<Grid2DInteractable>( ).IsUnMarked( ) ) {
                HasMadeValidMove = VerifyMove ( tform, Color.red );
                break;
            }
        }
        return HasMadeValidMove;
    }

    // base class needs an instance of 'endTurnEvent'
    protected override PlayerTurnExitEvent MadeValidMove ( ) {
        Player opponentPlayer = FindObjectOfType<PlayerHuman>();
        IPlayer nextPlayer = opponentPlayer.GetComponent<IPlayer>();
        IPlayerTurn nextPlayerTurn = opponentPlayer.GetComponent<IPlayerTurn>();

        return new PlayerTurnExitEvent ( nextPlayer, nextPlayerTurn );
    }

    private void GetGridReferenceForAI ( ) {
        grid = FindObjectOfType<Grid2D> ( );
    }

    /* 0 = valid move, 1 = ai, 2 = player*/
    private void MinMax() {
        for ( int i = 0; i < board.GetLength ( 0 ); i++ ) {
            for ( int j = 0; j < board.GetLength ( 1 ); j++ ) {
                print ( "going " + i + j );
                Vector2 current = new Vector2(i, j);
                Grid2DVertex gridV = grid.Grid2DData.VertexTable[current];
                if ( gridV.GetComponent<Grid2DInteractable> ( ).IsUnMarked ( ) ) {
                    board[i , j] = 0;
                } else if ( gridV.GetComponent<Grid2DInteractable> ( ).OwnerByID == 0 ) {
                    board[i , j] = 1;
                } else {
                    board[i , j] = -1;
                }
            }
        }
        print ( board );
    }

    private void ZeroBoard() {
        for ( int i = 0; i < board.GetLength ( 0 ); i++ ) {
            for ( int j = 0; j < board.GetLength ( 1 ); j++ ) {
                Debug.Log ( i + " " + j );
                board[i , j] = 0;
            }
        }
    }
}