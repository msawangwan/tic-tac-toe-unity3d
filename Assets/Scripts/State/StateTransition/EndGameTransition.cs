using UnityEngine;
using System.Collections;

public class EndGameTransition : IStateTransition {
    private GameObject gameBoardObject;

    public bool HasTriggered { get; private set; }
    public bool HasCompleted { get; private set; }

    public EndGameTransition ( GameRound endingRound ) {
        InitBools ( );
        gameBoardObject = endingRound.FetchBoardObjectRefernce ( );
    }

    public IEnumerable BeginTransition ( ) {
        HasTriggered = true;
        yield return FadeBoard( );
        HasCompleted = true;
    }

    private IEnumerator FadeBoard ( ) {
        float fadeMultiplier = 0.47f;
        IFadeableGameObject board = gameBoardObject.GetComponent<IFadeableGameObject>();
        IEnumerator fadingTransform = board.FadeOut( fadeMultiplier ).GetEnumerator();
        yield return fadingTransform;
    }

    private void InitBools ( ) {
        HasTriggered = false;
        HasCompleted = false;
    }
}
