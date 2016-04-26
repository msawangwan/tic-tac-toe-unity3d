using UnityEngine;
using System;
using System.Collections;

public interface IPlayer {
    bool IsTurnActive { get; } // init to false

    void EnterTurn ( );
    void TakeTurn ( );
    void ExitTurn ( );

    event Action<PlayerTurnExitEvent> ExitTurnEvent;
}
