using UnityEngine;
using System.Collections;

public class MusicMasterController : MonoBehaviour {
    private AudioSource menusource;
    private AudioSource playsource;

    private AudioClip menumusic;
    private AudioClip playmusic;

    void Start() {
        menumusic = Resources.Load<AudioClip> ( ResourcePath.mainTheme );
        playmusic = Resources.Load<AudioClip> ( ResourcePath.playTheme );

        defaultVol = .29f;

        menusource = gameObject.AddComponent<AudioSource> ( );
        menusource.playOnAwake = false;
        menusource.volume = defaultVol;
        menusource.clip = menumusic;

        playsource = gameObject.AddComponent<AudioSource> ( );
        playsource.playOnAwake = false;
        playsource.volume = defaultVol;
        playsource.clip = playmusic;
    }

    bool started = false;
    bool isMenuMusic;
    bool isPlayMusic;

    float defaultVol;
    float currentVol;
    float menuVol;
    float playVol;

    bool isFaded = false;

    public void MusicCheck ( bool needSwitch ) {
        bool changeTrack = needSwitch;

        if ( started == false ) {
            menusource.Play ( );

            isMenuMusic = true;
            started = true;
        }

        float fadetime = 2.7f;

        if (changeTrack == true) {
            if ( isMenuMusic == true ) {
                StartCoroutine ( CrossFade (menusource, playsource, fadetime ) );
                isMenuMusic = false;
                isPlayMusic = true;
            } else if ( isPlayMusic == true ) {
                StartCoroutine ( CrossFade (playsource,menusource, fadetime ) );
                isPlayMusic = false;
                isMenuMusic = true;
            }          
        }
    }

    IEnumerator CrossFade (AudioSource a1, AudioSource a2, float duration) {
        float start = Time.time;
        float end = start + duration;
        a2.Play ( );
        while (Time.time < end) {

            float i = (Time.time - start) / duration;
            a1.volume = defaultVol - i;
            a2.volume = Mathf.Clamp ( i , 0 , defaultVol );
            yield return null;
        }
    }
}
