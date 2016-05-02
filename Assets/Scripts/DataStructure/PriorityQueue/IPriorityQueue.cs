using System.Collections.Generic;
/// <summary>
/// Head is node with LOWEST priority VALUE
/// </summary>
/// <typeparam name="T"></typeparam>
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