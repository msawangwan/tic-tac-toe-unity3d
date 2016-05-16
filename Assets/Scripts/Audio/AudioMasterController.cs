using UnityEngine;
using System.Collections;

public class AudioMasterController : MonoBehaviour {
    public static AudioClip[] LoadScribbleSFX() {
        string[] paths = ResourcePath.GetScribbleFilepaths().ToArray();
        AudioClip[] loadedClips = new AudioClip[paths.Length];

        for ( int i = 0; i < paths.Length; i++ ) {
            loadedClips[i] = Resources.Load<AudioClip> ( paths[i] );
        }

        return loadedClips;
    }
}
