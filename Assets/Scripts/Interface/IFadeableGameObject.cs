using UnityEngine;
using System.Collections;

public interface IFadeableGameObject {
    IEnumerable FadeIn ( float fadeMultiplier );
    IEnumerable FadeOut ( float fadeMultiplier );
}
