using UnityEngine;
using System.Collections;

public interface IStateTransition {
    // make setters private
    bool hasTriggered { get; }
    bool hasCompleted { get; }

    IEnumerable Enter ( );
    IEnumerable Exit ( );
}
