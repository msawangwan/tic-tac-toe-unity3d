using UnityEngine;
using System.Collections;

public class PlayerTurnExitEvent {
    public IPlayer NextPlayer { get; private set; }
    public IPlayerTurn NextPlayerMove { get; private set; }

    public PlayerTurnExitEvent( IPlayer nextPlayer, IPlayerTurn nextPlayerMove ) {
        NextPlayer = nextPlayer;
        NextPlayerMove = nextPlayerMove;
    }
}
