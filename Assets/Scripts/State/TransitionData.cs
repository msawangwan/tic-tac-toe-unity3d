using UnityEngine;
using System.Collections;

public class TransitionData {
    public IEnumerable[] LoadedTransitions;

    public TransitionData(IEnumerable[] td) {
        LoadedTransitions = td;
    }
}
