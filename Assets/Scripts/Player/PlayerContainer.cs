using UnityEngine;
using System.Collections;
/// <summary>
/// Scene Object, used primarily for its transform.
/// 
/// Any player gameobject instance in the scene should
/// ALWAYS be childed to this container class.
/// </summary>
public class PlayerContainer : MonoBehaviour {
    public static void AttachToTransformAsChild(GameObject playerGameObject) {
        if (playerGameObject.GetComponent<Player>()) {
            PlayerContainer container = FindObjectOfType<PlayerContainer> ( );
            playerGameObject.transform.SetParent ( container.transform );
        }
    }
}
