using UnityEngine;
using System.Collections;

public class MenuFadeTransition : IStateTransition {
    GameObject menuToFade;

    public MenuFadeTransition() { }

    public MenuFadeTransition(GameObject menu) {
        menuToFade = menu;
    }

    public IEnumerable Enter () {
        yield return null;
    }

    public IEnumerable Exit (  ) {
        Debug.Log ( "[MenuFadeTransition][Exit] Fading menu ... " );

        GameObject fullScreenMenu = menuToFade;
        CanvasGroup menuAlpha = fullScreenMenu.GetComponent<CanvasGroup>();
        float fadeMultiplier = .5f;

        while ( menuAlpha.alpha > 0 ) {
            menuAlpha.alpha -= Time.deltaTime * fadeMultiplier;
            yield return null;
        }

        fullScreenMenu.SetActive ( false );
        menuAlpha.alpha = 1f;
    }
}