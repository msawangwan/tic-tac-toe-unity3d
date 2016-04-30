using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid2D : MonoBehaviour, IGrid2D {
    public bool HasTiles { get; private set; }

    public Grid2DObjectData grid2D { get; set; }
    protected Vector2[] gridVerticies;

    protected bool isDrawnToScreen { get; set; }

    /* Use as constructor, call on GameObject instantiation. */
    public void InitAsNew( Grid2DObjectData grid2D ) {
        this.grid2D = grid2D;
        isDrawnToScreen = false;
    }

    private Vector2[] SetVerticies() {
        // two ways to get keys to arr
        //Vector2[] verticies = new Vector2[grid2D.VertexTable.Count];
        //grid2D.VertexTable.Keys.CopyTo ( verticies , 0 );
        Vector2[] verticies = ( new List<Vector2>(grid2D.VertexTable.Keys)).ToArray();


        return verticies;
    }
}
