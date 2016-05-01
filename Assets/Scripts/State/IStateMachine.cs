using UnityEngine;
using System.Collections;

public interface IStateMachine {
    bool isExecuting { get; }
    void SetInitialState ( IState initialState );
}
