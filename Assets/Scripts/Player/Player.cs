using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Player : MonoBehaviour {
    protected Board gameboard;

    protected PlayerTurnController turn;
    protected PlayerMoveTable moves;

    public PlayerID playerID { get; private set; }

    public void InitPlayer(Board newBoard, PlayerID id) {
        gameboard = newBoard;
        playerID = id;

        turn = new PlayerTurnController ( gameboard , this );
        moves = new PlayerMoveTable();
    }

    public void MoveFirst (bool isTurnFirst) {
        Debug.Log ( gameObject.name + " moves first" );
    }

    public virtual bool ExecuteMove ( Vector2 move ) {
        moves.IncrementMove ( move );
        bool win = moves.CheckForTicTacToe( );
        return win;
    }

    //TODO: implement this as a Player protected or public method, and delete PlayerTurnController entirely
    //public bool ExecuteTurn ( Tile selectedTile ) {
        //Debug.Log ( "[PlayerTurnController][ExecuteTurn] Executing Turn." );
        //if ( !isRoundOver ) {
            //Vector2 selectedTilePosition = selectedTile.ReturnTilePosition();
            //if ( board.TileTable.ContainsKey ( selectedTilePosition ) ) {
                //if ( selectedTile.isAValidMove == true ) {
                    //Debug.Log ( "[PlayerTurnController][ExecuteTurn] Found a move." );
                    //selectedTile.MarkTileAsSelected ( playerByID );
                    //isRoundOver = player.ExecuteMove ( selectedTilePosition );
                    //return true;
                //}
            //}
        //} else {
            //Debug.Log ( "[PlayerTurnController][ExecuteTurn] Game is over." );
        //}
        //return false;
    //}

    protected abstract bool AttemptMove<T>( ) where T : Component;
}
