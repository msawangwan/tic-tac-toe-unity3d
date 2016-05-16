using UnityEngine;
using System.Collections;

public class LoadingTransition : IStateTransition {
    public bool HasTriggered { get; private set; }
    public bool HasCompleted { get; private set; }

    private IEnumerable[] loadedTransitions = null;
    private IEnumerable loadedTransition = null;

    /* Do nothing during transition. */
    public LoadingTransition ( ) {
        InitBools ( );
    }

    /* Load a coroutine to trigger during loading transition. */
    public LoadingTransition ( IEnumerable loadingTransition ) {
        InitBools ( );
        loadedTransition = loadingTransition;
    }

    /* Use this constructor to pass multiple coroutines. */
    public LoadingTransition(TransitionData d) {
        loadedTransition = d.LoadedTransitions;
    }

    /* Destroy a GameObject during loading transition. */
    public LoadingTransition ( GameObject objectToDestroy ) {
        InitBools ( );
        MonoBehaviour.Destroy ( objectToDestroy );
    }

    /* Implements IStateTransition. This sends an enumerator to the state machine. */
    public IEnumerable BeginTransition ( ) {
        HasTriggered = true;
        if ( loadedTransition != null ) {
            yield return loadedTransition.GetEnumerator ( );
            yield return null;
        } else if ( loadedTransitions != null ) {
            for ( int i = 0; i < loadedTransitions.Length; i++ ) {
                yield return loadedTransitions[i].GetEnumerator ( );
                yield return null;
            }
        } else {
            yield return new WaitForEndOfFrame ( );
        }
        HasCompleted = true;
    }

    /* Sets the bools to an initial state. */
    private void InitBools ( ) {
        HasTriggered = false;
        HasCompleted = false;
    }
}
