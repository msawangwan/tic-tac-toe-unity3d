using UnityEngine;
using UnityEngine.Assertions;

public class Utility {
    private bool timer_isTimer = false;                                 // sets to true if this class is instantiated via the constructor with an 'interval' parameter
    public float timer_tickInterval { get; private set; }         // duration between ticks
    public float timer_tickCount { get; private set; }            // number of ticks so far
    public float timer_elapsedSinceLastTick { get; private set; } // if this value is > than tickInterval, reset to 0
    

    public Utility() { }

    public Utility(float interval) {
        timer_tickInterval = interval;
        timer_isTimer = true;
    }

    public bool Timer(float interval = 0) {                       // call in any update or while loop
        if ( timer_isTimer == false ) {
            this.timer_tickInterval = interval;
        } 
            
        Assert.IsFalse ( timer_tickInterval == 0 );

        timer_elapsedSinceLastTick += Time.deltaTime;
        if(timer_elapsedSinceLastTick > timer_tickInterval) {
            Debug.Log ( "[Utility][Timer] Tick " + timer_tickCount + " (" + "gametime " + Time.time + ")" );
            timer_elapsedSinceLastTick = 0;
            ++timer_tickCount;
            return true;
        }
        return false;
    }
}
