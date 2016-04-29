using UnityEngine;
using System.Collections;

public class BoardContainer : MonoBehaviour {
    public static void AttachToTransformAsChild ( GameObject playerGameObject ) {
        if ( playerGameObject.GetComponent<Player> ( ) ) {
            BoardContainer container = FindObjectOfType<BoardContainer> ( );
            playerGameObject.transform.SetParent ( container.transform );
        }
    }

    //RUNNING TESTS delte if you forget about this
    //private void Awake() {
        //GameObject tilePrefab = Resources.Load<GameObject> ( ResourcePath.boardTile );
        //GameObject current = MonoBehaviour.Instantiate( tilePrefab , new Vector2(0,0) , Quaternion.identity ) as GameObject;
       // MonoBehaviour.Instantiate( tilePrefab , new Vector2(0,0) , Quaternion.identity );
        //print ( "TEST " + current.name);
       // print ( "TEST " + tilePrefab.name );
   // }
}
