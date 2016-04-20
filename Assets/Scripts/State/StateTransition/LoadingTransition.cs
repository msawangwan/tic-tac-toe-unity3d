using UnityEngine;
using System.Collections;

public class LoadingTransition : IStateTransition {
    public bool hasTriggered { get; private set; }
    public bool hasCompleted { get; private set; }
    public LoadingTransition ( ) {
        InitBools ( );
    }

    public IEnumerable Enter ( ) {
        hasTriggered = true;
        yield return new WaitForEndOfFrame ( );
        hasCompleted = true;
    }

    public IEnumerable Exit ( ) {
        hasTriggered = true;
        yield return new WaitForEndOfFrame ( );
        hasCompleted = true;
    }

    private void InitBools ( ) {
        hasTriggered = false;
        hasCompleted = false;
    }
}
