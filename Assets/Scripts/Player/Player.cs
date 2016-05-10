using UnityEngine;
using System;

public abstract class Player : MonoBehaviour, IPlayer {
    public int PlayerByID { get; private set; }
    public string PlayerName { get; private set; }

    public bool IsTurnActive { get; set; } // Note: switch to private set? 
    public bool IsWinner { get; private set; }

    /* Substitute for constructor, call on GameObject instantiantion. */
    public virtual void InitAsNew ( int id, string playerName ) {
        PlayerByID = id;
        PlayerName = playerName;

        IsTurnActive = false;
        IsWinner = false;
    }

    /* Call on start of each round! Resets player to fresh state for 
        a new round. Initialises moves. Allows player to persist between rounds. */
    public virtual void NewGameState ( ) {
        IsWinner = false;
    }

    /* Currently Not Being Called!!! */
    public void RoundOverState () {
        IsTurnActive = false;
        Destroy ( gameObject );
    }
}