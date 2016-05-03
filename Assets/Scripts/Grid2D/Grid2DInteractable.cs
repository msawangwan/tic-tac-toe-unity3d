using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Grid2DInteractable : MonoBehaviour {
    public int OwnerByID { get; private set; }
    public bool IsInteractable { get; private set; }

    public void InitOnStart ( ) {
        IsInteractable = true;
        OwnerByID = 1000; // set to some value that isn't reachable -- TODO: find better way
    }

    public bool IsUnMarked ( ) {
        if (IsInteractable == false) {
            return false;
        }
        IsInteractable = false;
        return true;
    }

    public void SetOwner ( int PlayerByID ) {
        OwnerByID = PlayerByID;
        Debug.Log ( gameObject.name + " is owned by playerID: " + PlayerByID );
    }
}