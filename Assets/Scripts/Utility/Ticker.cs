using UnityEngine;
using UnityEngine.Assertions;

public class Ticker {
    private bool isTimer = false;                           // sets to true if this class is instantiated via the constructor with an 'interval' parameter
    public float TickInterval { get; private set; }         // duration between ticks
    public float TickCount { get; private set; }            // number of ticks so far
    public float ElapsedSinceLastTick { get; private set; } // if this value is > than tickInterval, reset to 0
    

    public Ticker() { }

    public Ticker(float interval) {                              // call this constructor to initialise a timer
        TickInterval = interval;
        isTimer = true;
    }

    public bool Timer(float interval = 0) {                      // call in any update or while loop
        if ( isTimer == false ) {
            this.TickInterval = interval;
        } 
            
        Assert.IsFalse ( TickInterval == 0 );

        ElapsedSinceLastTick += Time.deltaTime;
        if(ElapsedSinceLastTick > TickInterval) {
            //Debug.Log ( "[Utility][Timer] Tick " + TickCount + " (" + "gametime " + Time.time + ")" );
            ElapsedSinceLastTick = 0;
            ++TickCount;
            return true;
        }
        return false;
    }
}