using UnityEngine;
using System;
using System.Collections;

public interface IPlayerTurn {
    bool IsTurnActive { get; } // init to false

    void EnterTurn ( );
    bool TakeTurn ( );
    void ExitTurn ( );

    event Action<PlayerTurnExitEvent> ExitTurnEvent;
}
