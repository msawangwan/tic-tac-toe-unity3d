using UnityEngine;

public class PlayerComputer : Player, IPlayerMove {
    public bool HasMadeValidMove { get; private set; }

    private Grid2D grid;

    public override void NewGameState ( ) {
        base.NewGameState ( );
        GetGridReferenceForAI ( );
    }

    protected override bool AttemptMove<T>() {
        HasMadeValidMove = false;
        foreach ( Transform v in grid.Grid2DData.GridObject.transform ) {         
            if ( v.GetComponent<Grid2DInteractable>( ).IsUnMarked( ) ) {
                HasMadeValidMove = VerifyMove ( v.transform, Color.red );
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