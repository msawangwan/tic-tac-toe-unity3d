using UnityEngine;
using System.Collections;
/// <summary>
/// TODO: Refactor
/// </summary>
public class MenuExitTransition : IStateTransition {
    private GameObject menuToFade;

    public bool HasTriggered { get; private set; }
    public bool HasCompleted { get; private set; }

    public MenuExitTransition ( ) {
        InitBools ( );
    }

    // constructor with specific UI menu object
    public MenuExitTransition ( GameObject menu ) {
        InitBools ( );
        menuToFade = menu;
    }

    public IEnumerable BeginTransition ( ) {
        HasTriggered = true;
        GameObject fullScreenMenu = menuToFade;
        CanvasGroup menuAlpha = fullScreenMenu.GetComponent<CanvasGroup>();
        float fadeMultiplier = 1.8f;

        while ( menuAlpha.alpha > 0 ) {
            menuAlpha.alpha -= Time.deltaTime * fadeMultiplier;
            yield return null;
        }

        MonoBehaviour.Destroy ( fullScreenMenu );
        HasCompleted = true;
    }

    private void InitBools ( ) {
        HasTriggered = false;
        HasCompleted = false;
    }
}
