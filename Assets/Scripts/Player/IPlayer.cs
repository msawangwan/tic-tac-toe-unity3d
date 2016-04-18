using UnityEngine;
using System.Collections;

public interface IPlayer {
    PlayerID playerID { get; set; }
    void SetInitialTurn( PlayerID startingPlayer );
}
