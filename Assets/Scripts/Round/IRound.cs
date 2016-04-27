using UnityEngine;
using System.Collections;

public interface IRound {
    bool IsGameOver { get; }
    void StartNewRound ( IRound round );
    void EndCurrentRound ( );
}
