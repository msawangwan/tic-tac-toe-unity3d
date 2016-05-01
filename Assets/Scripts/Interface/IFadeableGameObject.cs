using UnityEngine;
using System.Collections;

public interface IFadeableGameObject {
    float fadeTime { get; }

    IEnumerable FadeIn ( );
    IEnumerable FadeOut ( );
}
