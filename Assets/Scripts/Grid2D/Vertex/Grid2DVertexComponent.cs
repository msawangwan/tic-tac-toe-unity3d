using UnityEngine;
using System.Collections;

public class Grid2DVertexComponent : MonoBehaviour {
    public Grid2DVertexNode vertexNode { get; private set; }
    public Vector2 vertexPos { get; private set; }

    public void InitOnStart() {
        vertexPos = ( new Vector2 ( transform.position.x , transform.position.y ) );
        vertexNode = new Grid2DVertexNode ( vertexPos );
    }
}