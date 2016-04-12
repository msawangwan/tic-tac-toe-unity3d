using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {
    private GameManager gamemanager;
    private GameObject mainMenu;
    private Button newGame;
    private Button settings;

    private void Start() {
        InitialiseDependencies( );
    }

    private void InitialiseDependencies() {
        gamemanager = FindObjectOfType<GameManager>( );
        mainMenu = GameObject.FindGameObjectWithTag( UITags.mainMenu );
    }

    public void NewGame() {
        if(mainMenu.activeSelf) {            
            StartCoroutine( FadeMenu( ) );
        }
    }

    public void ReturnToMainMenu() {
        if(!mainMenu.activeSelf) {
            mainMenu.SetActive( true );
        }
    }

    IEnumerator FadeMenu() {
        CanvasGroup menuAlpha = mainMenu.GetComponent<CanvasGroup>();
        float fadeMultiplier = .5f;
        while (menuAlpha.alpha > 0) {
            menuAlpha.alpha -= Time.deltaTime * fadeMultiplier;
            yield return null;
        }
        Debug.Log( "[UI MANAGER] Main menu fade, complete ... " );
        mainMenu.SetActive( false );
        menuAlpha.alpha = 1f;
    }
}
