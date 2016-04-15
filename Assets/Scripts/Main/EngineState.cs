using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class EngineState : MonoBehaviour {

    protected virtual void Start ( ) {

    }

    protected virtual void Update ( ) {

    }

    public abstract void Init ( );
    public abstract void Cleanup ( );

    public abstract void Pause ( );
    public abstract void Resume ( );

    public abstract void HandleEvents ( GameEngine gameegine ); // requires a reference to 'GameEngine'
    public abstract void UpdateState ( GameEngine gameegine );  // requires a reference to 'GameEngine'
    public abstract void Draw ( GameEngine gameegine );         // requires a reference to 'GameEngine'

    public void ChangeState ( GameEngine gameengine , EngineState gamestate ) {
        gameengine.ChangeState ( gamestate );
    }
}
