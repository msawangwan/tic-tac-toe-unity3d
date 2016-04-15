using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
    private GameEngine engine;
    Board board;
    GameManager game;
    UIManager ui;

	void Start () {
        GetReferences( );
        RunSetUp( );
    }

    void GetReferences() {
        // start the gameEngine
        print ( "first" );
        EngineState initialState = FindObjectOfType<States> ( ).GetComponent<StateInitialiseScene> ( ); // manually get the first state
        engine = FindObjectOfType<GameEngine> ( );
        engine.Init ( );
        engine.ChangeState ( initialState );

        board = FindObjectOfType<Board> ( );
        game = FindObjectOfType<GameManager> ( );
        ui = FindObjectOfType<UIManager> ( );
    }

    void RunSetUp() {
        board.SetupBoard( );

        game.StartGameManager( );
        ui.StartUIManager ( );

        MainCamera.SetCameraPosition( );
    }
}
