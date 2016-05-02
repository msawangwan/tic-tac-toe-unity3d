#define DEBUG
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// A Fast Priority Queque, based on implementation by 'BlueRaja'.
/// Modified by misha 'madmeesh' sawangwan.
/// 
/// Nodes must implement the class "PriorityQueueNode".
/// </summary>
/// <typeparam name="T"></typeparam>
public class PriorityQueue<T> : IPriorityQueue<T> where T : PriorityQueueNode {
    public T Head { get { return minHeap[1]; } }              // returns HEAD without dequeueing

    public int Count { get { return numNodes; } }             // returns current # of nodes in the queue
    public int MaxSize { get { return minHeap.Length - 1; } } // returns max # of nodes allowed

    private T[] minHeap;

    private int numNodes;
    private int numNodesEnqueued;

    public PriorityQueue ( int maxNodes ) {
        minHeap = new T[maxNodes + 1];
        numNodes = 0;
        numNodesEnqueued = 0;
    }

    public void Enqueue ( T node , float priority ) { // enqueue a node to the priority queue
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

        T previousLastNode = minHeap[numNodes];
        Swap ( node , previousLastNode );
        minHeap[numNodes] = null;
        numNodes--;

        OnNodeUpdated ( previousLastNode );
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

    /* Check if a given node enqueued. */
    public bool Contains ( T node ) {
#if DEBUG
        if ( node == null ) { throw new ArgumentNullException ( "node" ); }
        if ( node.QueueIndex < 0 || node.QueueIndex >= minHeap.Length ) { throw new InvalidOperationException ( "node.QueueIndex has been corrupted. Did you chane it manually? Or add this node to another queue?" ); }
#endif
        if ( minHeap[node.QueueIndex] == node )
            return true;
        return false;
    }

    public void Resize ( int maxNodes ) {
#if DEBUG
        if ( maxNodes <= 0 ) { throw new InvalidOperationException ( "Queue size cannot be smaller than 1." ); }
        if ( maxNodes < numNodes ) { throw new InvalidOperationException ( "Called Resize("+ maxNodes + "), but current queue contains " + numNodes + "nodes" ); }
#endif
        T[] resizedPqueue = new T[maxNodes + 1];
        int highestIndexToCopy = Math.Min(maxNodes,numNodes);

        for ( int i = 1; i < highestIndexToCopy; i++ ) {
            resizedPqueue[i] = minHeap[i];
        }

        minHeap = resizedPqueue;
    }

    public IEnumerator<T> GetEnumerator() {
        for ( int i = 1; i <= numNodes; i++ )
            yield return minHeap[i];
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator ( );
    }

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
            Debug.Log ( "parentIndex: " + parentIndex );
            T parent = minHeap[parentIndex];
            Debug.Log ( "parent: " + parent );

            if ( HasHigherPriority( parent, newLeft ) )
                break;

            Swap ( parent , newLeft );
            parentIndex = newLeft.QueueIndex / 2;
        }
    }

    /* Heapify-Down */
    private void CascadeDown(T node) {
        T newParent;

        int currentIndex = node.QueueIndex; // this variable is more commonly known as the 'final index' -- find out why!
        while ( true ) {
            newParent = node;
            int childLeftIndex = 2 * currentIndex;

            if ( childLeftIndex > numNodes ) { // is left-child higher P than node current?
                node.QueueIndex = currentIndex;
                minHeap[currentIndex] = node;
                break;
            }

            T childLeft = minHeap[childLeftIndex];
            if ( HasHigherPriority ( childLeft , newParent ) ) {
                newParent = childLeft;
            }

            int childRightIndex = childLeftIndex + 1;
            if ( childRightIndex <= numNodes ) { // is right-child higher P than node current or left child?
                T childRight = minHeap[childRightIndex];
                if ( HasHigherPriority ( childRight , newParent ) ) {
                    newParent = childRight;
                }
            }

            if ( newParent != node ) { // if child node left or right has higher P, swap and go through loop again
                minHeap[currentIndex] = newParent;

                int tempIndex = newParent.QueueIndex;
                newParent.QueueIndex = currentIndex;
                currentIndex = tempIndex;
            } else {
                node.QueueIndex = currentIndex;
                minHeap[currentIndex] = node;
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
    public bool IsValidQueue() {
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