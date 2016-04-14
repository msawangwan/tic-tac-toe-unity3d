using UnityEngine;
using System.Collections;

public abstract class State : MonoBehaviour {
    protected GameManager gamemanager;

    protected virtual void Start() {
        InitialiseState( );
    }

    protected virtual void InitialiseState() {
        gamemanager = FindObjectOfType<GameManager>( );
    }
}
