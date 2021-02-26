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
        SettingsSystem.SetAnisotropicToggler(aniosotropicToggler);
        SettingsSystem.SetFullScreenToggler(fullScreenToggler);
        SettingsSystem.SetVSyncToggler(vsyncToggler);

        SettingsSystem.SetMusicToggler(musicToggler, mixer, musicFloat);
        SettingsSystem.SetSoundFXToggler(sfxToggler, mixer, sfxFloat);
        SettingsSystem.SetMusicVolumeSlider(musicSlider, mixer, musicFloat);
        SettingsSystem.SetSoundFXVolumeSlider(sfxSlider, mixer, sfxFloat);

        SettingsSystem.SetAntialiasingDropdown(antialiasing);
        SettingsSystem.SetResolutionDropdown(resolution);
        SettingsSystem.SetShadowQualityDropdown(shadowQuality);
        SettingsSystem.SetShadowResolutionDropdown(shadowResolution);
        SettingsSystem.SetTextureDropdown(texture);
        SettingsSystem.SetQualityDropdown(quality, QualitySettings.GetQualityLevel());
        SettingsSystem.Refresh();
    }
}
