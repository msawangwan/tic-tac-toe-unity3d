using UnityEngine;
using System.Collections;

public abstract class State : MonoBehaviour {
    protected StateManager statemanager;
    protected GameManager gamemanager;
    protected UIManager uimanager;

    protected bool isActiveState;

    protected virtual void InitialiseState() {
        statemanager = FindObjectOfType<StateManager> ( );
        gamemanager = FindObjectOfType<GameManager>( );
        uimanager = FindObjectOfType<UIManager> ( );
    }

    protected virtual void Start() {
        InitialiseState ( );
    }

    // maybe protected???
    public abstract void UpdateState ( );
}
