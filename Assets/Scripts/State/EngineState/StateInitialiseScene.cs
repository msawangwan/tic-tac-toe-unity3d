using UnityEngine;
using System.Collections;
/// <summary>
/// CALLED ONCE AT THE START OF PROGRAM
/// - handles initialising of objects
/// - responsible for getting references of all managers
/// - passes them off to whoever needs them
/// </summary>
public class StateInitialiseScene : EngineState {
    private GameEngine engine;
    private Board board;
    private UI ui;

    private bool isDoneLoading;

    protected override void Start ( ) {
        isDoneLoading = false;
        base.Start ( );
    }

    protected override void Update ( ) {
        base.Update ( );
    }

    public override void Init ( ) {
        Debug.Log ( " how INIT" );
        SetSceneObjectReferences ( );
    }

    public override void Cleanup ( ) {}

    public override void Pause ( ) {}
    public override void Resume ( ) {}

    public override void HandleEvents ( GameEngine gameegine ) {}

    public override void UpdateState ( GameEngine gameegine ) {
        print ( "state set" );
        if (engine == null) {
            engine = gameegine;
        }
        while(isDoneLoading == false) {
            SetSceneObjectReferences ( );
        }       
    }

    public override void Draw ( GameEngine gameegine ) {}

    private void SetSceneObjectReferences() {
        //if(engine == null)
           // engine = FindObjectOfType<GameEngine> ( );
        if(board == null)
            board = FindObjectOfType<Board> ( );
        if(ui == null)
            ui = FindObjectOfType<UI> ( );

        if (board && ui) {
            Debug.Log ( "[StateInitialiseScene] Found references successfully." );
            isDoneLoading = true;
        }
    }
}
