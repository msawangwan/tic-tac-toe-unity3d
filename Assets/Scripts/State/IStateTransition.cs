using UnityEngine;
using System.Collections;

public interface IStateTransition {
    IEnumerable Exit ( );
    IEnumerable Enter ( );
}
