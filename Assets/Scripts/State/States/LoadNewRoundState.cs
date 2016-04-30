using UnityEngine;
using System;
using System.Collections.Generic;

public class LoadNewRoundState : IState {
    //private IRound round;

    private Dictionary<int, bool> playerTypes; // < id, isHumanPlayer >

    private Ticker delay;

    private float numTicksToDelay;
    private float numDelayTicks;

    private bool hasGameLoaded = false;

    public bool IsStateExecuting { get; private set; }

    public LoadNewRoundState( float loadTime ) {
        IsStateExecuting = true;

        playerTypes = new Dictionary<int , bool> ( );

        playerTypes.Add ( 0 , true );
        playerTypes.Add ( 1 , false );

        delay = new Ticker ( .6f );
        numTicksToDelay = loadTime;
        numDelayTicks = 0;

        hasGameLoaded = false;
    }

    public void EnterState ( ) { }

    public void ExecuteState ( ) {
        if ( hasGameLoaded == true ) {
            IsStateExecuting = false;
            OnGameSetupCompleted ( );
        }

        if ( delay != null ) {
            delay.Timer ( );
            numDelayTicks = delay.TickCount;
        }
                
        if (numDelayTicks > numTicksToDelay && hasGameLoaded == false) {
            //IGameBoard newBoard = new GameBoard(3,3);

            // TO DOOOOO !!! do all this in a new ROUND
            IConfigureable newGameBoard = new Grid2DConfiguration(3,3);
            IConfigureable newPlayers = new PlayerConfiguration(playerTypes);

            //List<IConfig> boardDataAsList = newGameBoard.Configure();
            Grid2DObjectData boardData = (Grid2DObjectData) newGameBoard.Configure()[0];
            Player p1Data = (Player) newPlayers.Configure()[0];
            Player p2Data = (Player) newPlayers.Configure()[1];

            //round = new GameRound( newBoard );

            delay = null;
            hasGameLoaded = true;
        }
    }

    public event Action<StateBeginExitEvent> RaiseStateChangeEvent;

    private void OnGameSetupCompleted ( ) {
        IState nextState = new RoundState( round );
        IStateTransition transition = new LoadingTransition();
        StateBeginExitEvent exitEvent = new StateBeginExitEvent(nextState, transition);

        if ( RaiseStateChangeEvent != null ) {
            RaiseStateChangeEvent ( exitEvent );
        }
    }
}
