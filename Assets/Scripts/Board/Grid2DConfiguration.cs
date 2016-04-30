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

    public List<IConfig> ObjectConfigData { get; private set; }

    public Grid2DConfiguration ( int sizeX, int sizeY ) {
        GridWidth = sizeX;
        GridHeight = sizeY;

        vertices = new Vector2[GridWidth * GridHeight];
        vertexTable = new Dictionary<Vector2, Grid2DVertex> ( );
        ObjectConfigData = new List<IConfig> ( );

        

        vertexTable.Clear ( );
        ObjectConfigData.Clear ( );
    }

    public List<IConfig> Configure() {
        Instantiate2DGridObject ( );

        gridData = new Grid2DObjectData {
            GridObject = grid2D,
            GridReference = grid2D.GetComponent<Grid2D> ( ),
            VertexTable = vertexTable,
            CenterPoint = GridCenterPoint
        };

        ObjectConfigData.Add ( gridData );
        return ObjectConfigData;
    }

    /* Instantiate an instance of the grid as a GameObject
       and instantiate each vertix of the grid as a child GameObject */
    private void Instantiate2DGridObject() {
        grid2D = MonoBehaviour.Instantiate<GameObject> ( new GameObject ( ) );
        grid2D.name = "Grid2D";
        grid2D.AddComponent<Grid2D> ( );
        
        BoardContainer.AttachToTransformAsChild ( grid2D );

        CalculateVerticies ( );
        FillVerticies ( grid2D );
    }

    /* Find the position of each vertex */
    private void CalculateVerticies ( ) {
        for ( int i = 0, x = 0; x < GridWidth; x++ ) {
            for ( int y = 0; y < GridHeight; y++, i++ ) {
                vertices[i] = new Vector2 ( x, y );
            }
        }
    }

    /* Fill each vertex of the grid with a GameObject */
    private void FillVerticies ( GameObject grid2D ) {
        GameObject vertex;

        int boardDimensions = vertices.Length;
        for ( int i = 0; i < boardDimensions; i++ ) {
            vertex = MonoBehaviour.Instantiate<GameObject> ( new GameObject ( ) );

            vertex.AddComponent<Grid2DVertex> ( );
            vertex.SetActive ( false );

            vertex.transform.SetParent ( grid2D.transform );
            vertex.transform.position = vertices[i];
            vertex.transform.rotation = Quaternion.identity;

            vertexTable.Add ( vertices[i], vertex.GetComponent<Grid2DVertex> ( ) );
        }
    }
}
/// <summary>
/// Related sister class
/// </summary>
public class Grid2DObjectData : IConfig {
    public GameObject GridObject { get; set; }
    public Grid2D GridReference { get; set; }
    public Dictionary<Vector2, Grid2DVertex> VertexTable { get; set; }
    public Vector2 CenterPoint { get; set; }
}

