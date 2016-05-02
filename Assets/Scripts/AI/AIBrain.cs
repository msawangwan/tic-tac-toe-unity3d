using UnityEngine;
using System.Collections;

public class AIBrain {
    private Grid2D grid;
    private Grid2DNode[] targets;

    private int numTargets;

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

        numTargets = grid.Grid2DData.GridObject.transform.childCount;
        targets = new Grid2DNode[numTargets];

        Grid2DNode newTargetNode;
        for ( int i = 0; i < numTargets; i++ ) {
            newTargetNode = grid.Grid2DData.GridObject.transform.GetChild ( i ).gameObject.AddComponent<Grid2DNode> ( );
            newTargetNode.SetPosition (( int ) newTargetNode.transform.position.x , (int) newTargetNode.transform.position.y );
            targets[i] = newTargetNode;
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
