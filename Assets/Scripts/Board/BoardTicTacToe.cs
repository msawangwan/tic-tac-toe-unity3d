using UnityEngine;
using System.Collections;

public class BoardTicTacToe : MonoBehaviour {
    private IGrid boardRef;

    public void SetBoardReference(IGrid boardRef ) {
        this.boardRef = boardRef;
    }

    public void SpawnBoard() {
        //boardRef.Draw ( );
    }
}
