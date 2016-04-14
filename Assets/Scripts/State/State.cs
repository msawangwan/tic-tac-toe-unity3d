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

        //float updateInterval = .5f;
        //while(true) {
        //    UpdateState ( );
        //    yield return new WaitForSeconds ( updateInterval );
       // }
    }

    // maybe protected???
    public abstract void UpdateState ( );
}
