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

    public IUIEvent buttonEvent;         // child classes should implement this interface AND init this reference using the 'this' keyword

    public UserInterfaceMenu ( ) {
        uiCanvasReference = MonoBehaviour.FindObjectOfType<Canvas> ( );
        isWaitingForInput = true;
    }

    public void MakeActiveInScene ( ) {
        if ( menuObject )
            menuObject.SetActive ( true );
    }

    protected abstract void MapButtons ( );

    protected void FindButtonsInChildren ( ) {
        if ( menuObject ) {
            menuButtons = menuObject.GetComponentsInChildren<Button> ( true );
            MapButtons ( );
        }
    }
}
