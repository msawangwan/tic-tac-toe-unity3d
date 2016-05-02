using UnityEngine;
using System.Collections;

/// <summary>
/// Base class for a Priority Queue Node.
/// </summary>
public interface IPriorityQueueNode {
    float Priority { get; set; }     // set BEFORE enqueueing the node
    int InsertionIndex { get; set; } // represents the order this node was inserted in - do not set outside of queue class
    int QueueIndex { get; set; }     // current node position - do not set outside of queue class
}

/// <summary>
/// For faster look ups, extend rather than implement.
/// </summary>
public class PriorityQueueNode : AStarValueNode {
    public float Priority { get; set; }
    public int InsertionIndex { get; set; }
    public int QueueIndex { get; set; }
}

/// <summary>
/// Extend PriorityQueueNode with this.
/// </summary>
public class AStarValueNode : MonoBehaviour {
    /* Cost Function: f = g + h */
    public AStarValueNode parentNode { get; set; } // <-- init to null? find a way to represent neighbors?? use x and y?
    public int x { get; private set; } // position of node in the x
    public int y { get; private set; } // position of node in the y
    public float StarValue { get; set; } // StarValue = g + h
    public float g { get; set; } // cost to reach this node, aka number of squares from current to this node
    public float h { get; set; } // estimate as to how much it will cost to reach the goal (win condition) from this node

    public void SetPosition(int xPos, int yPos) {
        x = xPos;
        y = yPos;
    }
}