using UnityEngine;
using System.Collections.Generic;

// for a graph see:
// http://www.redblobgames.com/pathfinding/a-star/implementation.html#csharp
// ----------------

public class Pathfinder {
    private Grid2D grid;
    private Grid2DNode[] nodes;

    private PriorityQueue<Grid2DNode> unvisited;
    private PriorityQueue<Grid2DNode> visited;

    private Dictionary<Grid2DNode, Grid2DNode> cameFrom;
    private Dictionary<Grid2DNode, int> costSoFar;

    private int numNodes;
    private bool hasStarted;

    public Pathfinder ( Grid2D grid, Grid2DNode[] nodes ) {
        this.grid = grid;
        this.nodes = nodes;

        numNodes = nodes.Length;

        unvisited = new PriorityQueue<Grid2DNode> ( numNodes );
        visited = new PriorityQueue<Grid2DNode> ( numNodes );

        cameFrom = new Dictionary<Grid2DNode, Grid2DNode> ( );
        costSoFar = new Dictionary<Grid2DNode, int> ( );

        hasStarted = false;
    }

    /* A STAR */
    public void FindPath ( Grid2DNode n_start, Grid2DNode n_goal ) { // another param is the graph
        Grid2DNode n_current = null;
        Grid2DNode n_neighbor = null;
        float rank = 0.0f;
        float cost = 0.0f;

        visited.Clear ( );
        unvisited.Clear ( );

        unvisited.Enqueue ( n_start , 0 );
        costSoFar[n_start] = 0;


        // TODO: add null, duplicate, out-of-bounds and empty checks
        while ( unvisited.Count > 0 ) { // <-- TODO: fix condition: should be "while lowest rank in OPEN is not the GOAL"
            n_current = unvisited.Dequeue ( );

            Grid2DVertex v;
            Vector2 p = new Vector2 (n_current.x, n_current.y);
            if ( grid.Grid2DData.VertexTable.ContainsKey ( p ) ) {
                v = grid.Grid2DData.VertexTable[p];
            } else {
                continue;
            }
            

            if ( n_current.x == n_goal.x && n_current.y == n_goal.y ) { // TODO: early exit ...
                return;
            }

            visited.Enqueue ( n_current , n_current.Priority );

            foreach ( Vector2 n in grid.Neighbors ( v ) ) { // <-- TODO: fix condition: should be "for neighbors of current"
                n_neighbor.parent = n_current.parent;
                cost = n_current.g + MovementCost ( n_current , n_neighbor );

                if ( unvisited.Contains ( n_neighbor ) ) {
                    if ( cost < n_neighbor.g ) {
                        unvisited.Remove ( n_neighbor );
                        break;
                    }
                }

                if ( visited.Contains ( n_neighbor ) ) {
                    if ( cost < n_neighbor.g ) {
                        visited.Remove ( n_neighbor );
                        break;
                    }
                }

                if ( !unvisited.Contains ( n_neighbor ) ) {
                    if ( !visited.Contains ( n_neighbor ) ) {
                        n_neighbor.g = cost;
                        rank = n_neighbor.g + n_neighbor.h;
                        unvisited.Enqueue ( n_neighbor , n_neighbor.Priority );
                        n_neighbor.parent = n_current;
                    }
                } //TODO ...Reconstruct reverse path from goal to start by following parent pointers ... ??
            }
        }
    }

    // TODO ... aka Heuristic is non-generic
    private float MovementCost( Grid2DNode start, Grid2DNode end ) {
        return Mathf.Abs ( start.x - end.x ) + Mathf.Abs ( start.y - end.y );
        //return start.StarValue + end.StarValue;
    }
}