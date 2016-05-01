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
    public bool HasVertexGameObjects { get; private set; }

    /* Use as constructor, call on Component instantiation. 
        Depends on Grid2DConfiguration and Grid2DObjectData. */
    public void InitOnStart ( Grid2DObjectData grid2DData ) {
        this.Grid2DData = grid2DData;
        HasVertexGameObjects = true;
    }
}