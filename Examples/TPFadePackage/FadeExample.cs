using TP.Framework.Unity;
using UnityEngine;
using UnityEngine.UI;

/// <summary> Wrapper to serialize interface </summary>
[System.Serializable]
public class AlphaFader : Fader<AlphaFadeActivator> { }

public class FadeExample : MonoBehaviour
{
    [SerializeField] private Button fadeButton;
    [SerializeField] private AlphaFader fader;

    // Use this for initialization
    private void Awake()
    {
        fadeButton.onClick.AddListener(() => {
            FadeSystem.Fade(fader);
        });
    }
}
