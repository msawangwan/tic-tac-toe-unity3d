using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Player : MonoBehaviour, IPlayer {   
    protected GameManager gamemanager;
    protected Board gameboard;
    protected Moves moveCounter;

    public PlayerID playerID { get; set; }
    public bool isTurn = false;

    protected virtual void Awake( ) {
        gamemanager = FindObjectOfType<GameManager>( );
        gameboard = FindObjectOfType<Board>( );
        moveCounter = GetComponent<Moves>( );
    }

    protected virtual void Update() {
        if (!isTurn) {
            return;
        } else {
            MakeAMove<Tile>( );
        }
    }

    protected abstract void MakeAMove<T>( ) where T : Component;

    public virtual bool UpdateMoveTable(Vector2 move) {
        moveCounter.IncrementMove( move );
        bool win = moveCounter.CheckForThree( );
        return win;
    }

    // at the start of a round, this is called for all players -- only called once perround
    public virtual void SetInitialTurn( PlayerID startingPlayer ) {
        if (startingPlayer == playerID) {
            isTurn = true;
        } else {
            isTurn = false;
        }
    }

    public virtual void StartTurn( ) {
        if (!isTurn)
            isTurn = true;
    }

    public virtual void EndTurn() {
        if (isTurn)
            isTurn = false;
    }
}
