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

    public bool InBounds ( Vector2 vertex ) {
        if ( vertex.x >= 0 && vertex.x < Grid2DData.xDimension )
            if ( vertex.y >= 0 && vertex.y < Grid2DData.yDimension )
                return true;
        return false;
    }

    // TODO: needs implementation or do in tictactoe..
    public virtual bool IsValid ( Vector2 vertex ) {
        // get the 'interactable component' and check 'if marked'
        return true;
    }

    // TODO: decide on vertex or nodes or a common interface...?
    public IEnumerable<Vector2> Neighbors(Grid2DVertex v ) {
        foreach ( Vector2 dir in Directions ) {
            Vector2 next = new Vector2 (v.X + dir.x, v.Y + dir.y);
            if (InBounds(next) && IsValid(next)) {
                yield return next;
            }
        }
    }
}