using UnityEngine;
using System.Collections.Generic;

public class Grid2DConfiguration : IConfigureable {
    private GameObject grid2D;

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

    public Grid2DConfiguration ( int sizeX, int sizeY ) {
        GridWidth = sizeX;
        GridHeight = sizeY;

        vertices = new Vector2[GridWidth * GridHeight];
        vertexTable = new Dictionary<Vector2, Grid2DVertex> ( );

        vertexTable.Clear ( );
    }

    public Grid2DObjectData GetGrid2DData () {
        Instantiate2DGridObject ( );

        gridData = new Grid2DObjectData ( grid2D, grid2D.GetComponent<Grid2D> ( ), vertexTable, GridCenterPoint, GridWidth, GridHeight );

        return gridData;
    }

    /* Instantiate an instance of the grid as a GameObject
       and instantiate each vertex of the grid as a child GameObject. */
    private void Instantiate2DGridObject() {
        grid2D = new GameObject ( "Grid2D" );
        grid2D.AddComponent<Grid2D> ( );
        
        BoardContainer.AttachToTransformAsChild ( grid2D );

        CalculateVerticies ( );
        FillVerticies ( grid2D );
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
            //this.v = MonoBehaviour.Instantiate<GameObject> (new GameObject ( ));
            GameObject v = new GameObject ( "vertex " + i );
            v.AddComponent<Grid2DVertex> ( );
            v.SetActive ( false );

            v.transform.SetParent ( grid2D.transform );
            v.transform.position = vertices[i];
            v.transform.rotation = Quaternion.identity;

            v.GetComponent<Grid2DVertex> ( ).InitAsNew ( );

            vertexTable.Add ( vertices[i], v.GetComponent<Grid2DVertex> ( ) );
        }
    }
}
/// <summary>
/// Related sister class
/// </summary>
public class Grid2DObjectData : IConfig {
    public IGrid2D GridReference { get; private set; }
    public GameObject GridObject { get; private set; }
    public Vector2 CenterPoint { get; private set; }
    public Dictionary<Vector2, Grid2DVertex> VertexTable { get; private set; }
    public int xDimension { get; private set; }
    public int yDimension { get; private set; }

    public Grid2DObjectData(GameObject gridObject, Grid2D gridReference, Dictionary<Vector2, Grid2DVertex> vertexTable, Vector2 centerPoint, int x, int y) {
        GridObject = gridObject;
        GridReference = gridReference;
        VertexTable = vertexTable;
        CenterPoint = centerPoint;
        xDimension = x;
        yDimension = y;
    }
}

