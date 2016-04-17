using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
/// <summary>
/// Application Entry Point.
/// </summary>
public class Main : MonoBehaviour {
    private GameEngine engine;
    private StateMachine statemachine;

	private void Start () {
        engine = FindObjectOfType<GameEngine> ( );
        statemachine = FindObjectOfType<StateMachine> ( );
        statemachine.InitStateMachine ( new LoadApplicationState());
    }
}