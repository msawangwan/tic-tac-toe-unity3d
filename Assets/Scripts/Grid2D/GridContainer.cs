using UnityEngine;
using System.Collections;

public class GridContainer : MonoBehaviour {
    public static void AttachToTransformAsChild ( GameObject gridGameObject ) {
        if ( gridGameObject.GetComponent<Grid2D> ( ) ) {
            GridContainer container = FindObjectOfType<GridContainer> ( );
            gridGameObject.transform.SetParent ( container.transform );
        }
    }
}
