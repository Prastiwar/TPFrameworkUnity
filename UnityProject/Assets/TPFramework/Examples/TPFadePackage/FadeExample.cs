using TPFramework.Unity;
using UnityEngine;
using UnityEngine.UI;

/// <summary> Wrapper to serialize interface </summary>
[System.Serializable]
public class AlphaFader : TPFader<TPAlphaFade> { }

public class FadeExample : MonoBehaviour
{
    [SerializeField] private Button FadeButton;
    [SerializeField] private AlphaFader Fader;

    // Use this for initialization
    private void Start()
    {
        FadeButton.onClick.AddListener(() => {
            TPFade.Fade(Fader);
        });
    }
}
