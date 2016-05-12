using UnityEngine;
using System.Collections;

public class Grid2DVertexNode : Grid2DAStarNode {
    /* Constructor. */
    public Grid2DVertexNode(Vector2 nodePosition) {
        x = nodePosition.x;
        y = nodePosition.y;
    }
}

public abstract class Grid2DAStarNode {
    public Grid2DAStarNode parent { get; set; }

    public float x { get; protected set; } // position of node in the x
    public float y { get; protected set; } // position of node in the y

    public float f { get; set; } // f = g + h
    public float g { get; set; } // cost to reach this node, aka number of squares from current to this node
    public float h { get; set; } // estimate as to how much it will cost to reach the goal (win condition) from this node
}
