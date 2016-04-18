using UnityEngine;
using System.Collections;
/// <summary>
/// ToDO: Work on this to save typing ... 
/// </summary>
public static class Logger {
    public static void DebugToConsole(string objName, string methodName, string logMessage) {
        Debug.Log ( "[" + objName + "]" + "[" + methodName + "] " + logMessage );
    }
}
