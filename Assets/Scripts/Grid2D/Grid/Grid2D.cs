using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// A GameObject Component.
/// 
/// Represents a Grid2D in the scene, depends on 
/// Grid2DConfiguration and Grid2DObjectData.
/// </summary>

public class Grid2D : MonoBehaviour {
    public Grid2DObjectData Grid2DData { get; private set; }
    public Vector2[] Directions { get; private set; } // make static?

    public bool HasVertexGameObjects { get; private set; }

    /* Use as constructor, call on Component instantiation. 
        Depends on Grid2DConfiguration and Grid2DObjectData. */
    public void InitOnStart ( Grid2DObjectData grid2DData ) {
        this.Grid2DData = grid2DData;

        Directions = new Vector2[] {
            new Vector2 (1f,0f),  // right
            new Vector2 (-1f,0f), // left
            new Vector2 (0f,1f),  // up
            new Vector2 (0f,-1f), // down
        };

        HasVertexGameObjects = true;
    }

    public bool InBounds ( Vector2 point ) { // TODO: use lookup table as in IsValid
        if ( point.x >= 0 && point.x < Grid2DData.xDimension )
            if ( point.y >= 0 && point.y < Grid2DData.yDimension )
                return true;
        return false;
    }

    public bool IsValid ( Vector2 point ) {
        if (Grid2DData.VertexTable.ContainsKey( point ) ) {
            Grid2DVertex v = Grid2DData.VertexTable[point];
            if ( !v.gameObject.GetComponent<Grid2DInteractable> ( ).IsInteractable ) { // already marked?
                return false;
            } else {
                return true;
            }
        }
        return false; // out of bounds?
    }

    public IEnumerable<Vector2> Neighbors ( Grid2DVertex v ) { // may also want to use node?
        foreach ( Vector2 dir in Directions ) {
            Vector2 next = new Vector2 (v.vertexPos.x + dir.x, v.vertexPos.y + dir.y);
            if ( InBounds ( next ) && IsValid ( next ) ) {
                yield return next;
            }
        }
    }

    public int Cost( Grid2DVertex a, Grid2DVertex b ) {
        if ( a.gameObject.GetComponent<Grid2DInteractable> ( ).OwnerByID == 0 ) {
            return 1;
        } else if ( a.gameObject.GetComponent<Grid2DInteractable> ( ).OwnerByID == 1 ) {
            return -1;
        }
        return 5;
    }
}