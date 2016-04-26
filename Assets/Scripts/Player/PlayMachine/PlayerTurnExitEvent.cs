using UnityEngine;
using System.Collections;

public class PlayerTurnExitEvent {
    public IPlayer NextPlayer { get; private set; }

    public PlayerTurnExitEvent(IPlayer nextPlayer) {
        NextPlayer = nextPlayer;
    }
}
