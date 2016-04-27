using UnityEngine;
using System;
using System.Collections;

public interface IPlayer {
    //PlayerID PlayerByID { get; }
    int PlayerByID { get; }
    bool IsWinner { get; } // init to false
}
