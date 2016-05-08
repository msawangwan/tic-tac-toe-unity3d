using UnityEngine;
using System.Collections;

/// <summary>
/// Node objects of the Priority Queue must derive from PriorityQueueNode.
/// </summary>
public abstract class PriorityQueueNode {
    public float Priority { get; set; }
    public int InsertionIndex { get; set; }
    public int QueueIndex { get; set; }
}

/// <summary>
/// Can also be implemented rather than derived from (Have not tried) -- has some performance drawbacks.
/// </summary>
public interface IPriorityQueueNode {
    float Priority { get; set; }     // set BEFORE enqueueing the node
    int InsertionIndex { get; set; } // represents the order this node was inserted in - do not set outside of queue class
    int QueueIndex { get; set; }     // current node position - do not set outside of queue class
}