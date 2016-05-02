using UnityEngine;
using System;
using System.Collections;
/// <summary>
/// Test Components and methods using this class.
/// </summary>
public class TesterComponent : MonoBehaviour {
    PriorityQueue<Grid2DNode> testPQ; // <- TODO: instantiate monobehaviours during test

    /* Use to trigger functions or methods with the spacebar key. */
    private bool triggerd = false;
    public void SpaceBarTrigger ( ) {
        if ( Input.GetKeyDown ( KeyCode.Space ) ) {

            if ( triggerd == false ) {
                triggerd = true;
                for ( int i = 0; i < testPQ.MaxSize; i++ ) {
                    float randomMultiplier = ( UnityEngine.Random.Range ( 0 , 25 ) ) / 2;
                    print ( "ENQUE: " + i * randomMultiplier );
                    testPQ.Enqueue ( new Grid2DNode ( ) , i * randomMultiplier );
                    print ( "QUEUE COUNT: " + testPQ.Count );
                }
                print ( "Testing QUEUE VALID: " + testPQ.IsValidQueue ( ) );
                print ( "HEAD: " + testPQ.Head.Priority );
            } else {
                triggerd = false;
                for (int i = 0; i < testPQ.MaxSize; i++ ) {
                    Grid2DNode n = testPQ.Dequeue ( );
                    print ( "DEQUEUED: " + n.Priority );
                    print ( "QUEUE IS VALID: " + testPQ.IsValidQueue ( ) );
                }
            }
        }
    }

    /* Use to trigger functions or methods with the A key. */
    public void ButtonATrigger() {
        if ( Input.GetKeyDown ( KeyCode.A ) ) {
            if ( testPQ == null ) {
                return;
            } else {
                testPQ.Clear ( );
                print ( "QUEUE COUNT: " + testPQ.Count );
            }
        } 
    }

    //private void Start() {
        //testPQ = new PriorityQueue<Grid2DNode_Test> ( UnityEngine.Random.Range(25, 300) );
    //}

    //private void Update () {
     //   SpaceBarTrigger ( );
     //   ButtonATrigger ( );
    //}
}
