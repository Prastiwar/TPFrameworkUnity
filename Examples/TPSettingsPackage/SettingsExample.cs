using TP.Framework.Unity;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsExample : MonoBehaviour
{
    [Header("Toggles")]
    [SerializeField] private Toggle aniosotropicToggler;
    [SerializeField] private Toggle fullScreenToggler;
    [SerializeField] private Toggle vsyncToggler;

    [Header("Audio")]
    [SerializeField] private Toggle musicToggler;
    [SerializeField] private Toggle sfxToggler;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private string musicFloat;
    [SerializeField] private string sfxFloat;

    [Header("Dropdowns")]
    [SerializeField] private Dropdown antialiasing;
    [SerializeField] private Dropdown quality;
    [SerializeField] private Dropdown resolution;
    [SerializeField] private Dropdown shadowQuality;
    [SerializeField] private Dropdown shadowResolution;
    [SerializeField] private Dropdown texture;

    // Use this for initialization
    private void Start()
    {
        TPSettings.SetAnisotropicToggler(aniosotropicToggler);
        TPSettings.SetFullScreenToggler(fullScreenToggler);
        TPSettings.SetVSyncToggler(vsyncToggler);

        TPSettings.SetMusicToggler(musicToggler, mixer, musicFloat);
        TPSettings.SetSoundFXToggler(sfxToggler, mixer, sfxFloat);
        TPSettings.SetMusicVolumeSlider(musicSlider, mixer, musicFloat);
        TPSettings.SetSoundFXVolumeSlider(sfxSlider, mixer, sfxFloat);

        TPSettings.SetAntialiasingDropdown(antialiasing);
        TPSettings.SetResolutionDropdown(resolution);
        TPSettings.SetShadowQualityDropdown(shadowQuality);
        TPSettings.SetShadowResolutionDropdown(shadowResolution);
        TPSettings.SetTextureDropdown(texture);
        TPSettings.SetQualityDropdown(quality, QualitySettings.GetQualityLevel());
        TPSettings.Refresh();
    }
}
