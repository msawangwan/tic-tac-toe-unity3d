using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
    GameManager game;
    Board board;

	void Start () {
        GetReferences( );
        RunSetUp( );     
    }

    void GetReferences() {
        board = FindObjectOfType<Board>( );
        game = FindObjectOfType<GameManager>( );
    }

    void RunSetUp() {
        board.SetupBoard( );
        game.StartGameManager( );
        MainCamera.SetCameraPosition( );
    }
}
