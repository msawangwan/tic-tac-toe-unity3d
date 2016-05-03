#define DEBUG
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Data Structure: Priority Queque 
/// Implements: IPriorityQueue<typeparam name="T">PriorityQueueNode</typeparam>
/// </summary>
public class PriorityQueue<T> : IPriorityQueue<T> where T : PriorityQueueNode {
    public T Head { get { return minHeap[1]; } }              // returns HEAD without dequeueing

    public int Count { get { return numNodes; } }             // returns current # of nodes in the queue
    public int MaxSize { get { return minHeap.Length - 1; } } // returns max # of nodes allowed

    private T[] minHeap;

    private int numNodes;
    private int numNodesEnqueued;

    public PriorityQueue ( int maxNodesAllowed ) {
        minHeap = new T[maxNodesAllowed + 1];
        numNodes = 0;
        numNodesEnqueued = 0;
    }

    /* Enqueue a node. */
    public void Enqueue ( T node , float priority ) {
#if DEBUG
        if (node == null) { throw new ArgumentException ( "node" ); }
        if (numNodes >= minHeap.Length - 1) { throw new InvalidOperationException ( "Queue is full - node cannot be added: " + node + "." ); }
        if (Contains(node)) { throw new InvalidOperationException ( "Node is already enqueued: " + node + "."); }
#endif

        minHeap[++numNodes] = node;

        node.Priority = priority;
        node.QueueIndex = numNodes;
        node.InsertionIndex = numNodesEnqueued++;

        CascadeUp ( minHeap[numNodes] );
    }

    /* Returns the node with highest priority. */
    public T Dequeue ( ) {
#if DEBUG
        if (numNodes <= 0) { throw new InvalidOperationException ( "No node set to head - cannot call Dequeue on an empty queue." ); }
#endif
        T head = minHeap[1];
        Remove ( head );
        return head;
    }
           
    /* Delete contents of the priority queue. */          
    public void Clear ( ) {
        Array.Clear ( minHeap , 1 , numNodes );
        numNodes = 0;
    }
     
    /* Remove a node by swapping it with TAIL and then soriting old TAIL into the proper position. */               
    public void Remove ( T node ) {
#if DEBUG
        if ( node == null ) { throw new ArgumentNullException ( "node" ); }
        if ( !Contains ( node ) ) { throw new InvalidOperationException ( "Cannot call Remove() on a node which is not enqueued: " + node ); }
#endif
        if ( node.QueueIndex == numNodes ) { // early-exit if node-to-remove is already the TAIL
            minHeap[numNodes] = null;
            numNodes--;
            return;
        }

        T old_TAIL = minHeap[numNodes];
        Swap ( node , old_TAIL );
        minHeap[numNodes] = null;
        numNodes--;

        OnNodeUpdated ( old_TAIL );
    }
    
    /* Must be called on a node any time its priority changes while it's in the queue. */           
    public void UpdatePriority ( T node, float priority ) {
#if DEBUG
        if ( node == null ) { throw new ArgumentNullException ( "node" ); }
        if ( !Contains ( node ) ) { throw new InvalidOperationException ( "Cannot call UpdatePriority() on a node which is not enqueued: " + node ); }
#endif
        node.Priority = priority;
        OnNodeUpdated ( node );
    }           

    /* Check if a given node is enqueued. */
    public bool Contains ( T node ) {
#if DEBUG
        if ( node == null ) { throw new ArgumentNullException ( "node" ); }
        if ( node.QueueIndex < 0 || node.QueueIndex >= minHeap.Length ) { throw new InvalidOperationException ( "node.QueueIndex has been corrupted. Did you chane it manually? Or add this node to another queue?" ); }
#endif
        if ( minHeap[node.QueueIndex] == node )
            return true;
        return false;
    }

    /* Copy the contents of minHeap into a new array, resizedMinHeap. */
    public void Resize ( int maxNodes ) {
#if DEBUG
        if ( maxNodes <= 0 ) { throw new InvalidOperationException ( "Queue size cannot be smaller than 1." ); }
        if ( maxNodes < numNodes ) { throw new InvalidOperationException ( "Called Resize("+ maxNodes + "), but current queue contains " + numNodes + "nodes" ); }
#endif
        T[] resizedMinHeap = new T[maxNodes + 1];
        int highestIndexToCopy = Math.Min(maxNodes,numNodes);

        for ( int i = 1; i < highestIndexToCopy; i++ ) {
            resizedMinHeap[i] = minHeap[i];
        }

        minHeap = resizedMinHeap;
    }

    /* Implements IEnumerable. */
    public IEnumerator<T> GetEnumerator() {
        for ( int i = 1; i <= numNodes; i++ )
            yield return minHeap[i];
    }

    /* Implements IEnumerable. */
    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator ( );
    }

    /* Encapsulate swap routine. */
    private void Swap(T n1, T n2) {
        minHeap[n1.QueueIndex] = n2;
        minHeap[n2.QueueIndex] = n1;

        int swapIndex = n1.QueueIndex;
        n1.QueueIndex = n2.QueueIndex;
        n2.QueueIndex = swapIndex;
    }

    /* Heapify-Up */
    private void CascadeUp(T newLeft) {
        int parentIndex = newLeft.QueueIndex / 2;
        while ( parentIndex >= 1 ) {
            T parent = minHeap[parentIndex];

            if ( HasHigherPriority( parent, newLeft ) )
                break;

            Swap ( parent , newLeft );
            parentIndex = newLeft.QueueIndex / 2;
        }
    }

    /* Heapify-Down */
    private void CascadeDown(T node) {
        T newParent;

        int correctIndexInHeap = node.QueueIndex;
        while ( true ) {
            newParent = node;
            int childLeftIndex = 2 * correctIndexInHeap;

            if ( childLeftIndex > numNodes ) { 
                node.QueueIndex = correctIndexInHeap;
                minHeap[correctIndexInHeap] = node;
                break;
            }

            T childLeft = minHeap[childLeftIndex];
            if ( HasHigherPriority ( childLeft , newParent ) ) { // is left-child higher P than node current?
                newParent = childLeft;
            }

            int childRightIndex = childLeftIndex + 1;
            if ( childRightIndex <= numNodes ) { 
                T childRight = minHeap[childRightIndex];
                if ( HasHigherPriority ( childRight , newParent ) ) { // is right-child higher P than node current or left child?
                    newParent = childRight;
                }
            }

            if ( newParent != node ) { // if child node left or right has higher P, swap and go through loop again
                minHeap[correctIndexInHeap] = newParent;

                int tempIndex = newParent.QueueIndex;
                newParent.QueueIndex = correctIndexInHeap;
                correctIndexInHeap = tempIndex;
            } else {
                node.QueueIndex = correctIndexInHeap;
                minHeap[correctIndexInHeap] = node;
                break;
            }
        }
    }

    /* Only returns true if ( hi > lo ). */
    private bool HasHigherPriority ( T hi , T lo) {
        if ( hi.Priority < lo.Priority )
            return false;
        if ( hi.Priority == lo.Priority )
            if ( hi.InsertionIndex < lo.InsertionIndex )
                return false;
        return true;
    }

    /* Bubble up or down as appropriate. */
    private void OnNodeUpdated ( T node ) {
        int parentIndex = node.QueueIndex / 2;
        T parentNode = minHeap[parentIndex];

        if ( parentIndex > 0 && HasHigherPriority ( node , parentNode ) ) {
            CascadeUp ( node );
        } else { // Note: CascadeDown is called if parentNode == node (aka node is the root)
            CascadeDown ( node );
        }
    }

    /* For testing only -- validates the min-heap invariant. */
    public bool ValidateMinHeapInvariant() {
        for ( int i = 1; i < minHeap.Length; i++ ) {
            if(minHeap[i] != null) {
                int leftChildIdx = 2 * i;
                if ( leftChildIdx < minHeap.Length && minHeap[leftChildIdx] != null )
                    if ( HasHigherPriority ( minHeap[leftChildIdx] , minHeap[i] ) )
                        return false;
                int rightChildIdx = leftChildIdx + 1;
                if ( rightChildIdx < minHeap.Length && minHeap[rightChildIdx] != null )
                    if ( HasHigherPriority ( minHeap[rightChildIdx] , minHeap[i] ) )
                        return false;
            }
        }
        return true;
    }
}