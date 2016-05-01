using UnityEngine;
using System.Collections;

public class GridInteractableObject : MonoBehaviour {
    public bool IsInteractable { get; private set; }

    public void InitOnStart ( ) {
        IsInteractable = true;
    }

    public bool IsUnMarked ( ) {
        if (IsInteractable == false) {
            return false;
        }
        IsInteractable = false;
        return true;
    }
}
