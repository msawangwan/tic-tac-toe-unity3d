using UnityEngine;
using System;
using System.Collections;

public interface IPlayer {
    int PlayerByID { get; }
    string PlayerName { get; }
    bool IsWinner { get; } // init to false
}
