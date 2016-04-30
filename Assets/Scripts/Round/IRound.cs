using UnityEngine;
using System.Collections;

public interface IRound : IGrid2D {
    bool IsGameOver { get; }
    void StartNewRound ( );
    void EndCurrentRound ( );
}
