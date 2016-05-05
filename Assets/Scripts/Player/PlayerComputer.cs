using UnityEngine;
using System.Collections.Generic;

public class PlayerComputer : Player, IPlayerMove {
    public bool HasMadeValidMove { get; private set; }

    private Grid2D grid;
    private Pathfinder pathfinder;
    private Dictionary<Grid2DNode, Transform> movePriority;

    public override void NewGameState ( ) {
        base.NewGameState ( );
        GetGridReferenceForAI ( );
        pathfinder = new Pathfinder ( grid );
    }

    protected override bool AttemptMove<T>() {
        HasMadeValidMove = false;
        foreach ( Transform tform in grid.Grid2DData.GridObject.transform ) {         
            if ( tform.GetComponent<Grid2DInteractable>( ).IsUnMarked( ) ) {
                HasMadeValidMove = VerifyMove ( tform, Color.red );
                break;
            }
        }
        return HasMadeValidMove;
    }

    // base class needs an instance of 'endTurnEvent'
    protected override PlayerTurnExitEvent MadeValidMove ( ) {
        Player opponentPlayer = FindObjectOfType<PlayerHuman>();
        IPlayer nextPlayer = opponentPlayer.GetComponent<IPlayer>();
        IPlayerTurn nextPlayerTurn = opponentPlayer.GetComponent<IPlayerTurn>();

        return new PlayerTurnExitEvent ( nextPlayer, nextPlayerTurn );
    }

    private void GetGridReferenceForAI ( ) {
        grid = FindObjectOfType<Grid2D> ( );
    }
}