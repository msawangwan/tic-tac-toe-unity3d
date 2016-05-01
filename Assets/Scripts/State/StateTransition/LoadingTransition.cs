using UnityEngine;
using System.Collections;

public class LoadingTransition : IStateTransition {
    public bool HasTriggered { get; private set; }
    public bool HasCompleted { get; private set; }

    private IEnumerable loadTransition;

    /* Do nothing during transition. */
    public LoadingTransition ( ) {
        InitBools ( );
    }

    /* Load a coroutine to trigger during loading transition. */
    public LoadingTransition ( IEnumerable loadingTransition ) {
        InitBools ( );
        loadTransition = loadingTransition;
    }

    /* Destroy a GameObject during loading transition. */
    public LoadingTransition ( GameObject objectToDestroy ) {
        InitBools ( );
        MonoBehaviour.Destroy ( objectToDestroy );
    }

    public IEnumerable BeginTransition ( ) {
        HasTriggered = true;
        if (loadTransition != null) {
            yield return loadTransition.GetEnumerator ( );
            yield return null;
        } else {
            yield return new WaitForEndOfFrame ( );
        }
        HasCompleted = true;
    }

    private void InitBools ( ) {
        HasTriggered = false;
        HasCompleted = false;
    }
}
