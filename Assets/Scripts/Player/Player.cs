using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Player : MonoBehaviour, IPlayer {
    protected Board gameboard;
    protected GameTurn turns;
    protected Moves moves;

    public PlayerID playerID { get; private set; }
    public bool isAI = false;
    public bool isTurn = false;

    // called by GameRound
    public void InitPlayer(GameTurn turns, PlayerID id) {
        this.turns = turns;
        playerID = id;
    }

    // called by TurnMaker
    public void SetInitialTurn ( PlayerID startingPlayer ) {
        if ( startingPlayer == playerID ) {
            isTurn = true;
        } else {
            isTurn = false;
        }
    }

    public virtual bool MakeMove ( Vector2 move ) {
        moves.IncrementMove ( move );
        bool win = moves.CheckForThree( );
        return win;
    }


    public virtual void StartTurn ( ) {
        if ( !isTurn )
            isTurn = true;
    }

    public virtual void EndTurn ( ) {
        if ( isTurn )
            isTurn = false;
    }
    protected virtual void Awake( ) {
        gameboard = FindObjectOfType<Board>( );
        moves = GetComponent<Moves>( );
    }

    protected virtual void Update() {
        Debug.Log ( playerID + " is turn: " + isTurn );
        if (!isTurn) {
            return;
        } else {
            MakeAMove<Tile>( );
        }
    }

    protected abstract void MakeAMove<T>( ) where T : Component;
}
