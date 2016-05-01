using UnityEngine;
using System.Collections;

public interface IRound {
    string RoundWinner { get; }

    bool IsGameOver { get; }
    void StartNewRound ( );
    void EndCurrentRound ( string winner );
}
