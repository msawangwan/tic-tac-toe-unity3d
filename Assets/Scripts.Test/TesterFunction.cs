using UnityEngine;
using System;
using System.Collections;

public class TesterFunction : MonoBehaviour {
    private static bool triggerd = false;
    public static void SpaceBarTrigger ( Action methodToTest ) {
        if ( Input.GetKeyDown ( KeyCode.Space ) ) {
            if ( triggerd == false ) {
                triggerd = true;
                // for triggering functions with the space bar
            }
        }
    }
}
