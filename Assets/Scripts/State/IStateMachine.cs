using UnityEngine;
using System.Collections;

public interface IStateMachine {
    bool isExecuting { get; }
    void InitStateMachine ( IState initialState );
}
