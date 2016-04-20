using UnityEngine;
using System.Collections;

public interface IPlayer {
    PlayerID playerID { get; }
    void InitPlayer ( GameTurn turntaker , PlayerID id );
    void SetInitialTurn( PlayerID startingPlayer );
}
