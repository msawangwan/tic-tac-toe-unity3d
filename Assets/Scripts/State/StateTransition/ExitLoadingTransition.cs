using UnityEngine;
using System.Collections;

public class ExitLoadingTransition : IStateTransition {
    public ExitLoadingTransition() { }
    public IEnumerable Exit ( ) { yield return new WaitForEndOfFrame(); }
    public IEnumerable Enter ( ) { yield return new WaitForEndOfFrame(); }
}
