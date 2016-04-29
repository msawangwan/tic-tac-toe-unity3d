using UnityEngine;
using System;

public interface IUIEvent {
    event Action<StateBeginExitEvent> RaiseUIEvent;
}
