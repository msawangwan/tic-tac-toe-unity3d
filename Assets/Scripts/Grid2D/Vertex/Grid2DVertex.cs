using UnityEngine;
using System.Collections;

public class Grid2DVertex : MonoBehaviour, IVertex {
    protected Vector2 vertexPos { get; private set; }

    public virtual void InitOnStart() {
        vertexPos = ( new Vector2 ( transform.position.x , transform.position.y ) );
    }
}
