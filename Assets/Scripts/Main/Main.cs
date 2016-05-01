using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
/// <summary>
/// Application Entry Point.
/// </summary>
public class Main : MonoBehaviour {
    private StateMachine statemachine;

	private void Start () {
        statemachine = FindObjectOfType<StateMachine> ( );
        statemachine.SetInitialState ( new ApplicationLoadState());
    }
}