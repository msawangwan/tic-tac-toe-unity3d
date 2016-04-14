using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
    Board board;
    StateManager state;
    GameManager game;
    UIManager ui;

	void Start () {
        GetReferences( );
        RunSetUp( );
    }

    void GetReferences() {
        board = FindObjectOfType<Board> ( );
        state = FindObjectOfType<StateManager> ( );
        game = FindObjectOfType<GameManager> ( );
        ui = FindObjectOfType<UIManager> ( );

    }

    void RunSetUp() {
        board.SetupBoard( );

        state.StartStateManager ( );
        game.StartGameManager( );
        ui.StartUIManager ( );

        MainCamera.SetCameraPosition( );
    }
}
