using UnityEngine;
using UnityEngine.Assertions;

public class Utility {
    public float timer_tickInterval { get; private set; }         // duration between ticks
    public float timer_tickCount { get; private set; }            // number of ticks so far
    public float timer_elapsedSinceLastTick { get; private set; } // if this value is > than tickInterval, reset to 0

    public bool IntervalTimer(float interval) {                   // call in any update or while loop
        this.timer_tickInterval = interval;

        Assert.IsFalse ( timer_tickInterval == 0 );

        timer_elapsedSinceLastTick += Time.deltaTime;
        if(timer_elapsedSinceLastTick > timer_tickInterval) {
            Debug.Log ( "[UTILITY] Timer: Tick" + timer_tickCount + " (" + "gametime " + Time.time + ")" );
            timer_elapsedSinceLastTick = 0;
            ++timer_tickCount;
            return true;
        }
        return false;
    }
}
