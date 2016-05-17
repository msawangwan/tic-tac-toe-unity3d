using UnityEngine;
using System.Collections;

public class SFXMasterController : MonoBehaviour {
    private static float sfxPlayerVolume = .65f;

    public static AudioClip[] LoadScribbleSFX() {
        string[] paths = ResourcePath.GetScribbleFilepaths().ToArray();
        AudioClip[] loadedClips = new AudioClip[paths.Length];

        for ( int i = 0; i < paths.Length; i++ ) {
            loadedClips[i] = Resources.Load<AudioClip> ( paths[i] );
        }
        
        return loadedClips;
    }

    public static AudioClip[] LoadBloopSFX() {
        string[] paths = ResourcePath.GetBlipFilepaths().ToArray();
        AudioClip[] loadedClips = new AudioClip[paths.Length];

        for ( int i = 0; i < paths.Length; i++ ) {
            loadedClips[i] = Resources.Load<AudioClip> ( paths[i] );
        }

        return loadedClips;
    }

    public static void PlayContinueClip() {
        AudioClip playon = Resources.Load<AudioClip>(ResourcePath.sfxContinue);
        AudioSource player = FindObjectOfType<SFXMasterController>().GetComponent<AudioSource>();
        float prior = player.volume;

        player.volume = sfxPlayerVolume;
        player.PlayOneShot ( playon );
        player.volume = prior;
    }

    public static void PlayReadyClip() {
        AudioClip rdy = Resources.Load<AudioClip>(ResourcePath.sfxReady);
        AudioSource player = FindObjectOfType<SFXMasterController>().GetComponent<AudioSource>();
        float prior = player.volume;

        player.volume = sfxPlayerVolume;
        player.PlayOneShot ( rdy );
        player.volume = prior;
    }

    public static void PlayNoMercyClip ( ) {
        AudioClip nomercy = Resources.Load<AudioClip>(ResourcePath.sfxNoMercy);
        AudioSource player = FindObjectOfType<SFXMasterController>().GetComponent<AudioSource>();
        float prior = player.volume;

        player.volume = sfxPlayerVolume;
        player.PlayOneShot ( nomercy );
        player.volume = prior;
    }
}
