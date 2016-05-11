using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Grid Data Wrapper.
/// </summary>
public class Grid2D {
    public Grid2DComponent GridReference { get; private set; }
    public GameObject GridObject { get; private set; }
    public Vector2 CenterPoint { get; private set; }
    public Dictionary<Vector2, GameObject> VertexTable { get; private set; }
    public int xDimension { get; private set; }
    public int yDimension { get; private set; }

    /* Constructor. */
    public Grid2D ( GameObject gridObject, Grid2DComponent gridReference, Dictionary<Vector2, GameObject> vertexTable, Vector2 centerPoint, int x, int y ) {
        GridObject = gridObject;
        GridReference = gridReference;
        VertexTable = vertexTable;
        CenterPoint = centerPoint;
        xDimension = x;
        yDimension = y;

        gridReference.InitOnStart ( this );
    }
}
