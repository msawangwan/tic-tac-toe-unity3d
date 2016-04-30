using UnityEngine;
using System.Collections;

public interface IRound : IGrid {
    bool IsGameOver { get; }
    void StartNewRound ( );
    void EndCurrentRound ( );
}
