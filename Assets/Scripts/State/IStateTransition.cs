using UnityEngine;
using System.Collections;

public interface IStateTransition {
    bool HasTriggered { get; }
    bool HasCompleted { get; }
    
    IEnumerable BeginTransition ( );
}
