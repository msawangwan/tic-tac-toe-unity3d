using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class Grid2DInteractable : MonoBehaviour {
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
