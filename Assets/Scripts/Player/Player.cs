using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Player : MonoBehaviour, IPlayer {
    // TODO: duplicate statemachine to make a playerstatemachine
    protected Board gameboard;

    protected PlayerTurnController turn;
    protected PlayerMoveTable moves;

    protected bool hasMadeMove;

    public PlayerID playerID { get; private set; }

    public bool isAI { get; private set; }
    public bool isTurn { get; private set; }

    public void InitPlayer(Board newBoard, PlayerID id) {
        gameboard = newBoard;
        playerID = id;

        turn = new PlayerTurnController ( gameboard , this );
        moves = new PlayerMoveTable();

        isTurn = false;
    }

    public void MoveFirst (bool isTurnFirst) {
        isTurn = isTurnFirst;
        hasMadeMove = !isTurn;
        Debug.Log ( gameObject.name + " moves first" );
    }

    // TODO: use action or interface to signal a turn change!!
    public virtual void TakeTurn ( bool turnTaken ) {
        Debug.Log ( gameObject.name + " takes a turn" );
        isTurn = turnTaken;
    }

    public virtual bool UpdateMoveTable ( Vector2 move ) {
        moves.IncrementMove ( move );
        bool win = moves.CheckForTicTacToe( );
        return win;
    }

    protected virtual void Update() {
        Debug.Log ( playerID + " is turn: " + isTurn );
        if (!isTurn) {
            return;
        } else {
            AttemptMove<Tile>( );
        }
    }

    protected abstract void AttemptMove<T>( ) where T : Component;
}
