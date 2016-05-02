using UnityEngine;
using System.Collections;

public class Pathfinder {
    private Grid2DNode[] nodes;

    private PriorityQueue<Grid2DNode> set_open;
    private PriorityQueue<Grid2DNode> set_closed;

    private int numNodes;
    private bool hasStarted;

    public Pathfinder ( Grid2DNode[] nodes ) {
        this.nodes = nodes;

        numNodes = nodes.Length;

        set_open = new PriorityQueue<Grid2DNode> ( numNodes );
        set_closed = new PriorityQueue<Grid2DNode> ( numNodes );

        hasStarted = false;
    }

    // for a graph see:
    // http://www.redblobgames.com/pathfinding/a-star/implementation.html#csharp
    //

    /* A STAR */
    public void FindPath ( Grid2DNode n_start, Grid2DNode n_goal ) {
        Grid2DNode n_current = null;
        Grid2DNode n_neighbor = null;
        float rank = 0.0f;
        float cost = 0.0f;

        set_open.Enqueue ( n_start , 0 );
        set_closed.Clear ( );

        // TODO: add null, duplicate, out-of-bounds and empty checks
        while ( true ) { // <-- TODO: fix condition: should be "while lowest rank in OPEN is not the GOAL"
            n_current = set_open.Dequeue ( );

            if ( n_current.x == n_goal.x && n_current.y == n_goal.y ) { // TODO: early exit ...
                return;
            }

            set_closed.Enqueue ( n_current , n_current.Priority );

            while ( n_current.parentNode != null ) { // <-- TODO: fix condition: should be "for neighbors of current"
                n_neighbor.parentNode = n_current.parentNode;
                cost = n_current.g + MovementCost ( n_current , n_neighbor );

                if ( set_open.Contains ( n_neighbor ) ) {
                    if ( cost < n_neighbor.g ) {
                        set_open.Remove ( n_neighbor );
                        break;
                    }
                }

                if ( set_closed.Contains ( n_neighbor ) ) {
                    if ( cost < n_neighbor.g ) {
                        set_closed.Remove ( n_neighbor );
                        break;
                    }
                }

                if ( !set_open.Contains ( n_neighbor ) ) {
                    if ( !set_closed.Contains ( n_neighbor ) ) {
                        n_neighbor.g = cost;
                        rank = n_neighbor.g + n_neighbor.h;
                        set_open.Enqueue ( n_neighbor , n_neighbor.Priority );
                        n_neighbor.parentNode = n_current;
                    }
                }
            }

            //TODO ...
            // ... Reconstruct reverse path from goal to start by following parent pointers ... ??
        }       
    }

    // TODO ...
    private float MovementCost(Grid2DNode start, Grid2DNode end) {
        return Mathf.Abs ( start.x - end.x ) + Mathf.Abs ( start.y - end.y );
        //return start.StarValue + end.StarValue;
    }
}
