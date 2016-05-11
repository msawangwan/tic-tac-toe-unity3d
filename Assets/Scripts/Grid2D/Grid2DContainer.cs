using UnityEngine;
using System.Collections;

public class Grid2DContainer : MonoBehaviour {
    public static void AttachToTransformAsChild ( GameObject gridGameObject ) {
        if ( gridGameObject.GetComponent<Grid2DComponent> ( ) ) {
            Grid2DContainer container = FindObjectOfType<Grid2DContainer> ( );
            gridGameObject.transform.SetParent ( container.transform );
        }
    }
}
