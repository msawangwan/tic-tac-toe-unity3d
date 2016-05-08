using UnityEngine;

/// <summary>
/// Test Components and methods using this class.
/// </summary>
public class TesterComponent : MonoBehaviour {
    //priorityqueue<grid2dnode> testpq; // <- todo: instantiate monobehaviours during test

    ///* use to trigger functions or methods with the spacebar key. */
    //private bool triggerd = false;
    //public void spacebartrigger ( ) {
    //    if ( input.getkeydown ( keycode.space ) ) {
    //        if ( triggerd == false ) {
    //            triggerd = true;
    //            for ( int i = 0; i < testpq.maxsize; i++ ) {
    //                float randommultiplier = ( unityengine.random.range ( 0 , 25 ) ) / 2;
    //                print ( "enque: " + i * randommultiplier );
    //                testpq.enqueue ( new grid2dnode ( ) , i * randommultiplier );
    //                print ( "queue count: " + testpq.count );
    //            }
    //            print ( "testing queue valid: " + testpq.validateminheapinvariant ( ) );
    //            print ( "head: " + testpq.head.priority );
    //        } else {
    //            triggerd = false;
    //            for (int i = 0; i < testpq.maxsize; i++ ) {
    //                grid2dnode n = testpq.dequeue ( );
    //                print ( "dequeued: " + n.priority );
    //                print ( "queue is valid: " + testpq.validateminheapinvariant ( ) );
    //            }
    //        }
    //    }
    //}

    ///* use to trigger functions or methods with the a key. */
    //public void buttonatrigger() {
    //    if ( input.getkeydown ( keycode.a ) ) {
    //        if ( testpq == null ) {
    //            return;
    //        } else {
    //            testpq.clear ( );
    //            print ( "queue count: " + testpq.count );
    //        }
    //    } 
    //}

    ////private void start() {
    //    //testpq = new priorityqueue<grid2dnode_test> ( unityengine.random.range(25, 300) );
    ////}

    ////private void update () {
    // //   spacebartrigger ( );
    // //   buttonatrigger ( );
    ////}
}
