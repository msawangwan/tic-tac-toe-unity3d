using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class GameEngine : MonoBehaviour {
    private Stack<EngineState> stateStack;
    private GameEngine engine;
    private EngineState state;

    public bool Running { get; private set; }

    private void Update() {
        UpdateEngine ( );
    }

    public void Init() {
        engine = GetComponent<GameEngine> ( );    
    }

    public void Cleanup() {}

    public void ChangeState( EngineState nextState ) {
        state = nextState;
        state.Init ( );
    }

    public void PushState( EngineState stateToPush ) {}
    public void PopState() {
        // remove state top of the engine stack
    }

    public void HandleEvents() {
        // call the associated 'events' method from the state class at top of the stack
    }

    public void UpdateEngine() { 
        if(state == null) {
            Assert.IsFalse ( state == null );
            return;
        }
        Debug.Log ( "[GameEngine] Updating Engine" );
        print ( "current state " + state.name );
        state.UpdateState ( engine );

        // call the associated 'update' method from the state class at top of the stack
    }

    public void Draw() {
        // call the associated 'draw' method from the state class at top of the stack
    }

    public void Quit() {
        Running = false;
    }
}