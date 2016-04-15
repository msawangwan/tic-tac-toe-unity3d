using UnityEngine;
using System.Collections;

public interface IInjection {
    void SetGameObjectReference<T> ( GameObject reference ) where T : Component;
}
