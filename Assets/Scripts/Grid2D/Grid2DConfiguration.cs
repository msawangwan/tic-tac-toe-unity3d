using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// An object representation of a 2D Grid and related functions.
/// 
/// Has a sister class - Grid2DObjectData, used as a data container for
/// grid data. Use with a Grid2D Component to create a visual representation
/// of the 2Dgrid in a Unity3D scene.
/// </summary>
public class Grid2DConfiguration {
    private GameObject grid2DObject;

    private Vector2[] vertices;
    private Dictionary<Vector2, Grid2DVertex> vertexTable;

    private Grid2DObjectData gridData;

    public int GridWidth { get; private set; }
    public int GridHeight { get; private set; }

    public Vector2 GridCenterPoint {
        get {
            int numCoordinates = vertices.Length;
            Vector2 sum = new Vector2(0,0);

            foreach ( Vector2 coord in vertices ) {
                sum += coord;
            }

            float centerX = sum.x;
            float centerY = sum.y;
            centerX = centerX / numCoordinates;
            centerY = centerY / numCoordinates;

            return new Vector2 ( centerX, centerY );
        }
    }

    /* Constructor. */
    public Grid2DConfiguration ( int sizeX, int sizeY ) {
        GridWidth = sizeX;
        GridHeight = sizeY;

        vertices = new Vector2[GridWidth * GridHeight];
        vertexTable = new Dictionary<Vector2, Grid2DVertex> ( );

        vertexTable.Clear ( );
    }

    /* Returns an instance of Grid2DData. */
    public Grid2DObjectData GetGrid2DData () {
        Instantiate2DGridObject ( );
        return new Grid2DObjectData ( grid2DObject, grid2DObject.GetComponent<Grid2D> ( ), vertexTable, GridCenterPoint, GridWidth, GridHeight );
    }

    /* Instantiate an instance of the grid as a GameObject
       and instantiate each vertex of the grid as a child GameObject. */
    private void Instantiate2DGridObject() {
        grid2DObject = new GameObject ( "Grid2D" );
        grid2DObject.AddComponent<Grid2D> ( );
        
        Grid2DContainer.AttachToTransformAsChild ( grid2DObject );

        CalculateVerticies ( );
        FillVerticies ( grid2DObject );
    }

    /* Find the position of each vertex. */
    private void CalculateVerticies ( ) {
        for ( int i = 0, x = 0; x < GridWidth; x++ ) {
            for ( int y = 0; y < GridHeight; y++, i++ ) {
                vertices[i] = new Vector2 ( x, y );
            }
        }
    }

    /* Fill each vertex of the grid with a GameObject. */
    private void FillVerticies ( GameObject grid2D ) {

        int boardDimensions = vertices.Length;
        for ( int i = 0; i < boardDimensions; i++ ) {
            GameObject vObj = new GameObject ( "vertex " + i );
            Grid2DVertex vRef = vObj.AddComponent<Grid2DVertex> ( );

            vObj.transform.SetParent ( grid2D.transform );
            vObj.transform.position = vertices[i];
            vObj.transform.rotation = Quaternion.identity;

            vRef.InitOnStart ( );

            vertexTable.Add ( vertices[i], vRef );
        }
    }
}
/// <summary>
/// Related sister class.
/// 
/// Small package that holds reference to the current 2D Grid.
/// </summary>
public class Grid2DObjectData {
    public Grid2D GridReference { get; private set; }
    public GameObject GridObject { get; private set; }
    public Vector2 CenterPoint { get; private set; }
    public Dictionary<Vector2, Grid2DVertex> VertexTable { get; private set; }
    public int xDimension { get; private set; }
    public int yDimension { get; private set; }

    /* Constructor. */
    public Grid2DObjectData(GameObject gridObject, Grid2D gridReference, Dictionary<Vector2, Grid2DVertex> vertexTable, Vector2 centerPoint, int x, int y) {
        GridObject = gridObject;
        GridReference = gridReference;
        VertexTable = vertexTable;
        CenterPoint = centerPoint;
        xDimension = x;
        yDimension = y;

        gridReference.InitOnStart ( this );
    }
}

