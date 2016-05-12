using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Functions for creating a 2D-Grid.
/// </summary>
public class Grid2DConfiguration {
    private int GridWidth { get;  set; }
    private int GridHeight { get;  set; }

    private GameObject grid2DObject;

    private Vector2[] vertices;
    private Dictionary<Vector2, GameObject> vertexTable; /* <vertexPosition, vertexGameObject> */

    private Grid2D gridData;

    private bool areVertexInteractable = false;

    private Vector2 GridCenterPoint { get { return new Vector2 ( GridWidth / 2, GridHeight / 2 ); } }

    /* Constructor. */
    public Grid2DConfiguration ( int sizeX, int sizeY, bool vertexInteractable ) {
        GridWidth = sizeX;
        GridHeight = sizeY;

        areVertexInteractable = vertexInteractable;

        vertices = new Vector2[GridWidth * GridHeight];
        vertexTable = new Dictionary<Vector2, GameObject> ( );

        vertexTable.Clear ( );
    }

    /* Returns an instance of Grid2DData. */
    public Grid2D GetGrid2DData () {
        Instantiate2DGridObject ( );
        return new Grid2D ( grid2DObject, grid2DObject.GetComponent<Grid2DComponent> ( ), vertexTable, GridCenterPoint, GridWidth, GridHeight );
    }

    /* Instantiate an instance of the grid as a GameObject
       and instantiate each vertex of the grid as a child GameObject. */
    private void Instantiate2DGridObject() {
        grid2DObject = new GameObject ( "Grid2D" );
        grid2DObject.AddComponent<Grid2DComponent> ( );
        
        Grid2DContainer.AttachToTransformAsChild ( grid2DObject );

        CalculateVerticies ( );
        SpawnVerticies ( grid2DObject );
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
    private void SpawnVerticies ( GameObject grid2D ) {

        int boardDimensions = vertices.Length;
        for ( int i = 0; i < boardDimensions; i++ ) {
            GameObject vObj = new GameObject ( "vertex " + i );
            Grid2DVertexComponent vRef = vObj.AddComponent<Grid2DVertexComponent> ( );

            if ( areVertexInteractable ) {
                vObj.gameObject.AddComponent<BoxCollider2D> ( );
            }

            vObj.transform.SetParent ( grid2D.transform );
            vObj.transform.position = vertices[i];
            vObj.transform.rotation = Quaternion.identity;

            vRef.InitOnStart ( );

            vertexTable.Add ( vertices[i], vObj );
        }
    }
}