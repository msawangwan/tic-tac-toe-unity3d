using UnityEngine;
using UnityEngine.Assertions;
using System;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// - main gets and handles the initialising of the gameengine reference
/// - game engine, then enters setup mode (OnGameEngineStart)
/// </summary>
public class GameEngine : MonoBehaviour {
    public GameEngine engine { get; private set; }
    public StateMachine stateMachine { get; private set; }    
    public UI ui { get; private set; }

    public bool isInitialised { get; private set; }
    public bool isRunning { get; private set; }

    public void StartGameEngine ( ) { // constructor
        Debug.Log ( "[Game Engine][StartGameEngine] Initialising" );

        Debug.Log ( "[Game Engine][StartGameEngine] Init complete" );
    }

    public void UpdateEngine() { }
    public void HandleEvents ( ) { }

    public void Draw() {}
    public void Quit() { isRunning = false; }
}