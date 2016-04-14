using UnityEngine;
using System.Collections;

public class StateManager : MonoBehaviour {
    private State currentGameState;
    private State currentPlayerState;

    public void SetGameState ( GameState newGameState ) {
        currentGameState = newGameState;
    }

    public void SetPlayerState ( PlayerState newPlayerState ) {
        currentPlayerState = newPlayerState;
    }
}
