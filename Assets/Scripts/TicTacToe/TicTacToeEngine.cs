using UnityEngine;
using System.Collections.Generic;

public class TicTacToeEngine {
    public bool IsGameover = false;
    public string GameWinner { get; private set; }

    private Grid2DComponent grid;
    private TicTacToeBoard board;
    private Dictionary<Vector2, GameObject> cells;

    private Marker[][] boardState;

    private PlayerComputer p1_X;
    private PlayerHuman p2_O;

    private bool isSearching = false;

    public TicTacToeEngine( Grid2DComponent grid, Player p1, Player p2 ) {
        this.grid = grid;

        CreateGameBoard ( );

        p1_X = p1 as PlayerComputer;
        p2_O = p2 as PlayerHuman;
        p1_X.IsTurnActive = true;
        p2_O.IsTurnActive = false;
    }

    public void PlayTicTacToe ( ) {
        if ( IsGameover == false ) {
            if ( p1_X == null || p2_O == null ) {
                return;
            }

            if ( p1_X.IsTurnActive == true ) {
                TicTacToeMove bestMove = null;

                if ( isSearching == false ) {
                    bestMove = p1_X.NegaMaxMove ( board ); // call NEGAMAX tree-search
                    isSearching = true;
                }

                if ( bestMove != null ) {
                    boardState[(int) bestMove.Cell.x][(int) bestMove.Cell.y] = Marker.X;

                    cells[bestMove.Cell].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite> ( ResourcePath.x );

                    p1_X.IsTurnActive = false;
                    p2_O.IsTurnActive = true;

                    if ( board.CheckForWinner ( boardState, Marker.X ) ) {
                        GameWinner = "X won, you lose!";
                        IsGameover = true;
                    }
                    if ( board.CheckForDraw ( boardState ) ) {
                        GameWinner = "A draw, that's better than losing.";
                        IsGameover = true;
                    }

                    isSearching = false;
                } else {
                    Debug.Log ( "AI is searching for a move ... " );
                    return;
                }
            } else if ( p2_O.IsTurnActive == true ) {
                GameObject clicked = p2_O.ClickHandler<Grid2DVertexInteractable> ( );

                if ( clicked == null ) return;

                Vector2 m = clicked.transform.position;
                if ( boardState[(int) m.x][(int) m.y] == Marker.Blank ) { //move success
                    boardState[(int) m.x][(int) m.y] = Marker.O;

                    clicked.GetComponent<SpriteRenderer> ( ).sprite = Resources.Load<Sprite> ( ResourcePath.o );

                    if ( board.CheckForWinner ( boardState, Marker.O ) ) {
                        GameWinner = "O won, you win!";
                        IsGameover = true;
                    }
                    if ( board.CheckForDraw ( boardState ) ) {
                        GameWinner = "A draw, that's better than losing.";
                        IsGameover = true;
                    }

                    p2_O.IsTurnActive = false;
                    p1_X.IsTurnActive = true;
                }
            }
        } else {
            Debug.Log ( "GAMEOVER" );
        }
    }

    public void DestroyPlayers() {
        MonoBehaviour.Destroy ( MonoBehaviour.FindObjectOfType<PlayerHuman> ( ).transform.gameObject );
        MonoBehaviour.Destroy ( MonoBehaviour.FindObjectOfType<PlayerComputer> ( ).transform.gameObject );
    }

    private void CreateGameBoard ( ) {
        board = new TicTacToeBoard ( );
        cells = new Dictionary<Vector2, GameObject> ( );

        boardState = board.CreateEmptyBoard ( 3, 3 );
        board.Board = boardState;

        for ( int i = 0; i < boardState.Length; i++ ) {
            for ( int j = 0; j < boardState[i].Length; j++ ) {
                Vector2 cell = new Vector2 ( i, j );
                cells.Add ( cell, grid.Grid2DData.VertexTable[cell] );
            }
        }       
    }
}