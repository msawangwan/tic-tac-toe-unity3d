using UnityEngine;
using System.Collections;

public class LoadingTransition : IStateTransition {
    public bool HasTriggered { get; private set; }
    public bool HasCompleted { get; private set; }

    public LoadingTransition ( ) {
        InitBools ( );
    }

    // use this constructor if a gameobject needs to be removed from scene on state change
    public LoadingTransition ( GameObject objectToDestroy ) {
        InitBools ( );
        MonoBehaviour.Destroy ( objectToDestroy );
    }

    public IEnumerable BeginTransition ( ) {
        HasTriggered = true;
        yield return new WaitForEndOfFrame ( );
        HasCompleted = true;
    }

    private void InitBools ( ) {
        HasTriggered = false;
        HasCompleted = false;
    }
}
