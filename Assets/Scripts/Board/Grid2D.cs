using UnityEngine;
using System.Collections;

public class Grid2D : MonoBehaviour {
    private Grid2DObjectData grid2D;

    public bool HasTiles { get; private set; }
    public bool IsDrawnToScreen { get; private set; }

    public void Init( Grid2DObjectData grid2D ) {
        this.grid2D = grid2D;
    }
}
