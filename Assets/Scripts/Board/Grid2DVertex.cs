using UnityEngine;
using System.Collections;

public class Grid2DVertex : MonoBehaviour {
    protected Vector2 vertexPos { get; private set; }
    protected bool isDrawn { get; private set; }

    public virtual void InitAsNew() {
        gameObject.SetActive ( false );
        isDrawn = false;
        vertexPos = ( new Vector2 ( transform.position.x , transform.position.y ) );
    }
}
