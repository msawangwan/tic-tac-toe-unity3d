using UnityEngine;
using System.Collections;

public class Grid2DVertex : MonoBehaviour {
    public Grid2DNode vertexNode { get; private set; }
    public Vector2 vertexPos { get; private set; }

    public void InitOnStart() {
        vertexPos = ( new Vector2 ( transform.position.x , transform.position.y ) );
        vertexNode = new Grid2DNode ( vertexPos );
    }
}