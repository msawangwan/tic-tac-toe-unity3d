using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using System;
using System.Collections;

public abstract class UserInterfaceMenu : IUserInterface {
    protected GameObject menuObject;

    protected Canvas uiCanvasReference;
    protected Button[] menuButtons;

    protected bool isWaitingForInput;

    public IUIEvent buttonEvent;         // define in child

    public UserInterfaceMenu ( ) {
        uiCanvasReference = MonoBehaviour.FindObjectOfType<Canvas> ( );
        isWaitingForInput = true;
    }

    public void MakeActiveInScene ( ) {
        if ( menuObject )
            menuObject.SetActive ( true );
    }

    public bool Poll() {                 // currently, not implemented anywhere -- may not need this afterall ...
        if ( isWaitingForInput )
            return true;
        return false;
    }

    protected abstract void MapButtons ( );

    protected void FindButtonsInChildren ( ) {
        if ( menuObject ) {
            menuButtons = menuObject.GetComponentsInChildren<Button> ( true );
            MapButtons ( );
        }
    }
}
