using System.Collections.Generic;

/// <summary>
/// Priority Queque using the Min-Heap invariant:
/// 
/// - Uses a fixed-size array (for speed)
/// - Index starts at 1 not 0
/// - Left Child node is at [2 * i], if available
/// - Right Child node is at [2 * i + 1], if available
/// - Parent node is at [i / 2], if available
/// 
/// - Insert (Enqueue) -- O(logn)
/// - Remove (Dequeue) -- O(logn)
/// - Extract-Min (Head)- O(1)
/// - Contains() -------- O(1)
/// 
/// For best performance nodes should extend, rather than implement, a node base class.
/// </summary>
public interface IPriorityQueue<T> : IEnumerable<T> {
    void Enqueue ( T node , float priority );       // enqueue a node to the priority queue
    T Dequeue ( );                                  // remove and return the node at head of the queue
    void Clear ( );                                 // removes all items in the queue
    void Remove ( T node );                         // removes a node at its index (node needs to be in the queue)
    void UpdatePriority ( T node, float priority ); // recalculates the priority of a node

    bool Contains ( T node );                       // returns true if a given node is in the queue

    T Head { get; }                                 // returns the head without dequeueing
    int Count { get; }                              // returns number of nodes in the queue
}