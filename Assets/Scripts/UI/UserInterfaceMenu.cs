using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using System;
using System.Collections;

public abstract class UserInterfaceMenu : IUserInterface {
    protected GameObject menuObject;

    protected Canvas uiCanvasReference;
    protected Button[] menuButtons;

    protected AudioSource audioplayer;
    protected AudioClip btnClick;

    protected bool isWaitingForInput;

    public IUIEvent buttonEvent;         // child classes should implement this interface AND init this reference using the 'this' keyword

    public UserInterfaceMenu ( ) {
        uiCanvasReference = MonoBehaviour.FindObjectOfType<Canvas> ( );
        audioplayer = MonoBehaviour.FindObjectOfType<SFXMasterController> ( ).GetComponent<AudioSource> ( );

        btnClick = Resources.Load<AudioClip> ( ResourcePath.btn2 );

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
