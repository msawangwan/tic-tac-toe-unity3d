using UnityEngine;
using System;
using System.Collections;

public interface IPlayer {
    int PlayerByID { get; }
    bool IsWinner { get; } // init to false
}
