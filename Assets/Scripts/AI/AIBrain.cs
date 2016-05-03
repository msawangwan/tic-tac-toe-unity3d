using UnityEngine;
using System.Collections;

public class AIBrain {
    private Grid2D grid;
    private Grid2DNode[] targets;

    private int numNodes;

    /* Constructor. */
    public AIBrain() {

    }

    /* Constructor with grid paramter. */
    public AIBrain( Grid2D grid ) {
        this.grid = grid;
    }

    /*Get a reference to the grid, attach targets to each vertex and store a local reference. */
    public void GetGridReferenceAndTargets ( Grid2D grid ) {
        if ( grid == null )
            this.grid = grid;

        numNodes = grid.Grid2DData.GridObject.transform.childCount;
        targets = new Grid2DNode[numNodes];

        for ( int i = 0; i < numNodes; i++ ) {
            targets[i] = grid.Grid2DData.GridObject.transform.GetChild ( i ).GetComponent<Grid2DNode> ( );
        }
    }

    /*
      For number of verts
      Create a target
      A target has a point value
        */
    void CalculateTargets() {
        
    }
}
