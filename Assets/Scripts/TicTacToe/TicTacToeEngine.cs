using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TicTacToeEngine {
    public bool IsGameover = false;
    public string GameWinner { get; private set; }

    public WinVectors WinningCoordinates { get; private set; }

    private Grid2DComponent grid;
    private TicTacToeBoard board;
    private Dictionary<Vector2, GameObject> cells;

    private Marker[][] boardState;

    private PlayerComputer p1_X;
    private PlayerHuman p2_O;

    private AudioSource audioplayer;
    private AudioClip[] sfxbank;

    private bool isSearching = false;

    public TicTacToeEngine( Grid2DComponent grid, Player p1, Player p2 ) {
        this.grid = grid;

        CreateGameBoard ( );

        p1_X = p1 as PlayerComputer;
        p2_O = p2 as PlayerHuman;
        p1_X.IsTurnActive = false;
        p2_O.IsTurnActive = true;

        audioplayer = MonoBehaviour.FindObjectOfType<AudioMasterController> ( ).GetComponent<AudioSource> ( );

        sfxbank = AudioMasterController.LoadScribbleSFX ( );
    }

    private float turnDelay = 1.2f;
    private float t;
    private bool timed = false;

    public void PlayTicTacToe ( ) {
        if ( IsGameover == false ) {
            if ( p1_X == null || p2_O == null ) {
                return;
            }

            if ( p1_X.IsTurnActive == true ) {
                if ( timed == false ) {
                    t = turnDelay + Time.time;
                    timed = true;
                }
                 
                if ( Time.time < t ) {
                    return;
                }
                timed = false;

                TicTacToeMove bestMove = null;

                if ( isSearching == false ) {
                    bestMove = p1_X.NegaMaxMove ( board ); // NEGAMAX tree-search
                    isSearching = true;
                }

                if ( bestMove != null ) {                   
                    FireRandomScribbleSFX ( );
                    boardState[(int) bestMove.Cell.x][(int) bestMove.Cell.y] = Marker.X;

                    cells[bestMove.Cell].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite> ( ResourcePath.x );

                    p1_X.IsTurnActive = false;
                    p2_O.IsTurnActive = true;

                    if ( board.CheckForWinner ( boardState, Marker.X ) ) {
                        GameWinner = "You lost!";
                        WinningCoordinates = board.WinningCoordinates;
                        IsGameover = true;
                    }
                    if ( board.CheckForDraw ( boardState ) ) {
                        GameWinner = "A draw - better than losing.";
                        WinningCoordinates = null;
                        IsGameover = true;
                    }

                    isSearching = false;
                } else {
                    Debug.Log ( "AI is searching for a move ... " );
                    return;
                }
            } else if ( p2_O.IsTurnActive == true ) {
                GameObject clicked = p2_O.ClickHandler<Grid2DVertexComponent> ( );

                if ( clicked == null ) return;

                Vector2 m = clicked.transform.position;
                if ( boardState[(int) m.x][(int) m.y] == Marker.Blank ) { //move success
                    FireRandomScribbleSFX ( );
                    boardState[(int) m.x][(int) m.y] = Marker.O;

                    clicked.GetComponent<SpriteRenderer> ( ).sprite = Resources.Load<Sprite> ( ResourcePath.o );

                    if ( board.CheckForWinner ( boardState, Marker.O ) ) {
                        GameWinner = "You won!";
                        WinningCoordinates = board.WinningCoordinates;
                        IsGameover = true;
                    }
                    if ( board.CheckForDraw ( boardState ) ) {
                        GameWinner = "A draw - better than losing.";
                        WinningCoordinates = null;
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

    LineRenderer lr;

    Vector3 origin;
    Vector3 target;

    float counter = 0f;
    float distance;
    float drawSpeed = .8f;
    bool lineInitialised = false;

    public bool DrawWinningLine ( ) {
        if (WinningCoordinates == null) {
            return true;
        }

        if ( lineInitialised == false ) {
            lr = grid.Grid.GridObject.AddComponent<LineRenderer>();

            Vector2 from = WinningCoordinates.p1;
            Vector2 to = WinningCoordinates.p3;

            Vector2 heading = to - from;

            float extendedX_p1 = WinningCoordinates.p1.x;
            float extendedY_p1 = WinningCoordinates.p1.y;
            float extendedX_p2 = WinningCoordinates.p3.x;
            float extendedY_p2 = WinningCoordinates.p3.y;

            if ( heading.x == 0.0f ) { // extend vertically
                extendedY_p1 -= 1;
                extendedY_p2 += 1;
            } else if ( heading.y == 0.0f ) { // extend horizontally
                extendedX_p1 -= 1;
                extendedX_p2 += 1;
            } else if (heading.x == 2.0f && heading.y == 2.0f) { // extend in the diagonal
                extendedX_p1 -= 1;
                extendedY_p1 -= 1;
                extendedX_p2 += 1;
                extendedY_p2 += 1;
            } else if (heading.x == 2.0f && heading.y == -2.0f) { // extend in the other diagonal
                extendedX_p1 -= 1;
                extendedY_p1 += 1;
                extendedX_p2 += 1;
                extendedY_p2 -= 1;
            }

            origin = new Vector2 ( extendedX_p1, extendedY_p1 );
            target = new Vector2 ( extendedX_p2, extendedY_p2 );

            distance = Vector2.Distance ( origin, target );

            lr.material = Resources.Load<Material> ( ResourcePath.lineMat );
            lr.SetVertexCount ( 2 );
            lr.SetWidth ( .3f, .3f );
            lr.SetPosition ( 0, origin );
            lr.SetPosition ( 1, target );

            lineInitialised = true;
        }

        if (counter < distance) {
            counter += .1f / drawSpeed;
            float x = Mathf.Lerp(0, distance, counter);

            Vector2 p1 = origin;
            Vector2 p2 = target;
            Vector2 pointAlongLine = x * (Vector2)Vector3.Normalize(p2 - p1) + p1;

            lr.SetPosition ( 1, pointAlongLine );

            return false;
        } else {
            return true;
        }
    }

    private void CreateGameBoard ( ) {
        board = new TicTacToeBoard ( );
        cells = new Dictionary<Vector2, GameObject> ( );

        boardState = board.CreateEmptyBoard ( 3, 3 );
        board.Board = boardState;

        for ( int i = 0; i < boardState.Length; i++ ) {
            for ( int j = 0; j < boardState[i].Length; j++ ) {
                Vector2 cell = new Vector2 ( i, j );
                cells.Add ( cell, grid.Grid.VertexTable[cell] );
            }
        }       
    }

    private void FireRandomScribbleSFX() {
        int rand = UnityEngine.Random.Range(0, sfxbank.Length);
        audioplayer.PlayOneShot ( sfxbank[rand] );
    }
}