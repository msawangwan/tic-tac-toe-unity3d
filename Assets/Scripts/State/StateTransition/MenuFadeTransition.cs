using UnityEngine;
using System.Collections;

public class MenuFadeTransition : IStateTransition {
    private GameObject menuToFade;

    public bool hasTriggered { get; private set; }
    public bool hasCompleted { get; private set; }

    public MenuFadeTransition() {
        InitBools ( );
    }

    public MenuFadeTransition(GameObject menu) {
        InitBools ( );
        menuToFade = menu;
    }

    public IEnumerable Enter () {
        yield return null;
    }

    public IEnumerable Exit (  ) {
        Debug.Log ( "[MenuFadeTransition][Exit] Fading menu ... " );

        hasTriggered = true;
        GameObject fullScreenMenu = menuToFade;
        CanvasGroup menuAlpha = fullScreenMenu.GetComponent<CanvasGroup>();
        float fadeMultiplier = .5f;

        while ( menuAlpha.alpha > 0 ) {
            menuAlpha.alpha -= Time.deltaTime * fadeMultiplier;
            yield return null;
        }

        fullScreenMenu.SetActive ( false );
        menuAlpha.alpha = 1f;
        hasCompleted = true;
    }

    private void InitBools() {
        hasTriggered = false;
        hasCompleted = false;
    }
}