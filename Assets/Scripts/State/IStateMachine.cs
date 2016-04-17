using UnityEngine;
using System.Collections;

public interface IStateMachine {
    bool isStateMachineExecuting { get; }
    void InitStateMachine ( IState initialState );
}
