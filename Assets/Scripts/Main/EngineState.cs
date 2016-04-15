using UnityEngine;
using System.Collections;

public abstract class EngineState : MonoBehaviour {
    public abstract void Init ( );
    public abstract void Cleanup ( );

    public abstract void Pause ( );
    //public abstract void 

}
