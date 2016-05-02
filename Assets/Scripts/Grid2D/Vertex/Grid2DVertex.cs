using UnityEngine;
using System.Collections;

public class Grid2DVertex : MonoBehaviour {
    protected Vector2 vertexPos { get; private set; }
    public float X { get; private set; }
    public float Y { get; private set; }

    public virtual void InitOnStart() {
        vertexPos = ( new Vector2 ( transform.position.x , transform.position.y ) );

        X = vertexPos.x;
        Y = vertexPos.y;
    }
}
