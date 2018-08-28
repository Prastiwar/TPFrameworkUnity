/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TPFramework.Core;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace TPFramework.Unity
{
    public static class TPSettings
    {
        static TPSettings()
        {
            customQuality.CustomOption = new Dropdown.OptionData("Custom");
            customQuality.CustomQualityIndex = QualitySettings.names.ToList().IndexOf(customQuality.CustomOption.text);
        }

        [Serializable]
        public struct AudioParameters
        {
            public bool IsMusicOn;
            public bool IsSfxOn;
            public float MusicMixerFloat;
            public float SfxMixerFloat;
        }

        [Serializable]
        private struct CustomQuality
        {
            public int CustomQualityIndex;
            public Dropdown.OptionData CustomOption;
        }

        public static AudioParameters AudioSettings;

        private static readonly CustomQuality customQuality;
        private static readonly List<string> textureOptions          = new List<string> { "Very High", "High", "Medium", "Low" };
        private static readonly List<string> shadowQualityOptions    = new List<string> { "Disable", "HardOnly", "All" };
        private static readonly List<string> shadowResolutionOptions = new List<string> { "Low", "Medium", "High", "VeryHigh" };
        private static readonly List<string> antialiasingOptions     = new List<string> { "Disabled", "2x Multi Sampling", "4x Multi Sampling", "8x Multi Sampling" };
        private static readonly List<string> resolutionOptions       = new List<string>(Screen.resolutions.ToStringWithHZ());

        private static List<string> qualityOptions {
            get {
                List<string> list = QualitySettings.names.ToList();
                list.Remove("Custom");
                return list;
            }
        }

        private static Action refreshSettings = delegate { };
        private static Action onCustomQualitySet = delegate { };

        /// <summary> Adds listener to onValueChange that will change exposedParam in audioMixer to un-/mute </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)] // agressive inline
        public static void SetMusicToggler(Toggle toggle, AudioMixer audioMixer, string exposedParam, bool startValue = true)
        {
            toggle.onValueChanged.AddListener((value) => {
                AudioSettings.IsMusicOn = value;

                if (!AudioSettings.IsMusicOn) // cache float value before setting new value
                    AudioSettings.MusicMixerFloat = audioMixer.GetFloat(exposedParam);

                audioMixer.SetFloat(exposedParam, AudioSettings.IsMusicOn ? AudioSettings.MusicMixerFloat : -80);
            });
            toggle.isOn = startValue;
        }

        /// <summary> Adds listener to onValueChange that will change exposedParam in audioMixer to un-/mute </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)] // agressive inline
        public static void SetSoundFXToggler(Toggle toggle, AudioMixer audioMixer, string exposedParam, bool startValue = true)
        {
            toggle.onValueChanged.AddListener((value) => {
                AudioSettings.IsSfxOn = value;

                if (!AudioSettings.IsSfxOn) // cache float value before setting new value
                    AudioSettings.SfxMixerFloat = audioMixer.GetFloat(exposedParam);

                audioMixer.SetFloat(exposedParam, AudioSettings.IsSfxOn ? AudioSettings.SfxMixerFloat : -80);
            });
            toggle.isOn = startValue;
        }

        /// <summary> Adds listener to onClick that will change exposedParam in audioMixer to un-/mute and will change image to music-off/on </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)] // agressive inline
        public static void SetMusicToggler(Button button, Image image, Sprite musicOff, Sprite musicOn, AudioMixer audioMixer, string exposedParam, bool startValue = true)
        {
            AudioSettings.IsMusicOn = !startValue;
            button.onClick.AddListener(() => {
                AudioSettings.IsMusicOn = !AudioSettings.IsMusicOn;
                image.sprite = AudioSettings.IsMusicOn ? musicOn : musicOff;

                if (!AudioSettings.IsMusicOn) // cache "old" float value before setting new value
                    AudioSettings.MusicMixerFloat = audioMixer.GetFloat(exposedParam);

                audioMixer.SetFloat(exposedParam, AudioSettings.IsMusicOn ? AudioSettings.MusicMixerFloat : -80);
            });
        }

        /// <summary> Adds listener to onClick that will change exposedParam in audioMixer to un-/mute and will change image to sfx-off/on </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)] // agressive inline
        public static void SetSoundFXToggler(Button button, Image image, Sprite sfxOff, Sprite sfxOn, AudioMixer audioMixer, string exposedParam, bool startValue = true)
        {
            AudioSettings.IsSfxOn = !startValue;
            button.onClick.AddListener(() => {
                AudioSettings.IsSfxOn = !AudioSettings.IsSfxOn;
                image.sprite = AudioSettings.IsSfxOn ? sfxOn : sfxOff;

                if (!AudioSettings.IsSfxOn) // cache "old" float value before setting new value
                    AudioSettings.SfxMixerFloat = audioMixer.GetFloat(exposedParam);

                audioMixer.SetFloat(exposedParam, AudioSettings.IsSfxOn ? AudioSettings.SfxMixerFloat : -80);
            });
        }

        /// <summary> Adds listener to onValueChanged that will change volume of exposedParam in audioMixer </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)] // agressive inline
        public static void SetMusicVolumeSlider(Slider slider, AudioMixer audioMixer, string exposedParam, float startValue = 1, float minValue = -60, float maxValue = 25)
        {
            slider.onValueChanged.AddListener((value) => {
                audioMixer.SetFloat(exposedParam, value);
                AudioSettings.MusicMixerFloat = audioMixer.GetFloat(exposedParam);
            });
            SetSliderValues(slider, startValue, minValue, maxValue);
        }

        /// <summary> Adds listener to onValueChanged that will change volume of exposedParam in audioMixer </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)] // agressive inline
        public static void SetSoundFXVolumeSlider(Slider slider, AudioMixer audioMixer, string exposedParam, float startValue = 1, float minValue = -60, float maxValue = 25)
        {
            slider.onValueChanged.AddListener((value) => {
                audioMixer.SetFloat(exposedParam, value);
                AudioSettings.SfxMixerFloat = audioMixer.GetFloat(exposedParam);
            });
            SetSliderValues(slider, startValue, minValue, maxValue);
        }

        /// <summary> Adds listener to onValueChanged that will turn on/off Fullscreen  </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)] // agressive inline
        public static void SetFullScreenToggler(Toggle toggle, bool startValue = false)
        {
            toggle.onValueChanged.AddListener(SetFullScreen);
            toggle.isOn = startValue;
        }

        /// <summary> Adds listener to onValueChanged that will turn on/off VSync  </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)] // agressive inline
        public static void SetVSyncToggler(Toggle toggle, bool startValue = false)
        {
            toggle.onValueChanged.AddListener(SetVSync);
            toggle.isOn = startValue;
        }

        /// <summary> Adds listener to onValueChanged that will turn on/off Anisotropic  </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)] // agressive inline
        public static void SetAnisotropicToggler(Toggle toggle, bool startValue = false)
        {
            toggle.onValueChanged.AddListener(SetAnisotropic);
            toggle.isOn = startValue;
        }

        /// <summary> Adds listener to onValueChanged that will change Screen Resolution  </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)] // agressive inline
        public static void SetResolutionDropdown(Dropdown dropdown, int startIndex = 0, List<string> options = null)
        {
            AddDropdownOptions(dropdown, startIndex, options ?? resolutionOptions);
            dropdown.onValueChanged.AddListener((index) => {
                Resolution resolution = options != null ? options[index].ToResolution() : Screen.resolutions[index];
                Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
            });
        }

        /// <summary> Adds listener to onValueChanged that will change masterTextureLimit (sets quality to 'Custom') </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)] // agressive inline
        public static void SetTextureDropdown(Dropdown dropdown, int startIndex = 0, List<string> options = null)
        {
            AddDropdownOptions(dropdown, startIndex, options ?? textureOptions);
            dropdown.onValueChanged.AddListener(SetToCustomLevel);
            dropdown.onValueChanged.AddListener(SetTexture);
            refreshSettings += () => DropdownRefresher(dropdown, () => dropdown.value = QualitySettings.masterTextureLimit);
        }

        /// <summary> Adds listener to onValueChanged that will change Shadow Quality (sets quality to 'Custom') </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)] // agressive inline
        public static void SetShadowQualityDropdown(Dropdown dropdown, int startIndex = 0)
        {
            AddDropdownOptions(dropdown, startIndex, shadowQualityOptions);
            dropdown.onValueChanged.AddListener(SetToCustomLevel);
            dropdown.onValueChanged.AddListener(SetShadowQuality);
            refreshSettings += () => DropdownRefresher(dropdown, () => dropdown.value = (int)QualitySettings.shadows);
        }

        /// <summary> Adds listener to onValueChanged that will change Shadow Resolution (sets quality to 'Custom') </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)] // agressive inline
        public static void SetShadowResolutionDropdown(Dropdown dropdown, int startIndex = 0)
        {
            AddDropdownOptions(dropdown, startIndex, shadowResolutionOptions);
            dropdown.onValueChanged.AddListener(SetToCustomLevel);
            dropdown.onValueChanged.AddListener(SetShadowResolution);
            refreshSettings += () => DropdownRefresher(dropdown, () => dropdown.value = (int)QualitySettings.shadowResolution);
        }

        /// <summary> Adds listener to onValueChanged that will change AntiAliasing (sets quality to 'Custom') </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)] // agressive inline
        public static void SetAntialiasingDropdown(Dropdown dropdown, int startIndex = 0)
        {
            AddDropdownOptions(dropdown, startIndex, antialiasingOptions);
            dropdown.onValueChanged.AddListener(SetToCustomLevel);
            dropdown.onValueChanged.AddListener(SetAntialiasing);
            refreshSettings += () => DropdownRefresher(dropdown, () => dropdown.value = QualitySettings.antiAliasing == 8 ? 3 : QualitySettings.antiAliasing >> 1);
        }

        /// <summary> Adds listener to onValueChanged that will change Quality Level </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)] // agressive inline
        public static void SetQualityDropdown(Dropdown dropdown, int startIndex = 0)
        {
            AddDropdownOptions(dropdown, startIndex, qualityOptions);
            dropdown.onValueChanged.AddListener((index) => {
                SetQuality(index);
                refreshSettings();
            });

            onCustomQualitySet = () => {
                dropdown.options.Add(customQuality.CustomOption);
                dropdown.value = customQuality.CustomQualityIndex;
                dropdown.options.Remove(customQuality.CustomOption);
            };

            refreshSettings();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)] // agressive inline
        public static TPQualityLevel GetCurrentQualityLevel()
        {
            return new TPQualityLevel() {
                MasterTextureLimit = QualitySettings.masterTextureLimit,
                ShadowQuality = QualitySettings.shadows,
                ShadowResolution = QualitySettings.shadowResolution,
                AnisotropicFiltering = QualitySettings.anisotropicFiltering,
                Antialiasing = QualitySettings.antiAliasing,
                Resolution = Screen.currentResolution,
                VSync = QualitySettings.vSyncCount.ToBool(),
                FullScreen = Screen.fullScreen
            };
        }

        /// <summary> Change Quality Level and refresh affected settings </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)] // agressive inline
        public static void SetQualityLevel(TPQualityLevel level)
        {
            QualitySettings.masterTextureLimit = level.MasterTextureLimit;
            QualitySettings.shadowResolution = level.ShadowResolution;
            QualitySettings.shadows = level.ShadowQuality;
            QualitySettings.anisotropicFiltering = level.AnisotropicFiltering;
            QualitySettings.antiAliasing = level.Antialiasing;
            QualitySettings.vSyncCount = level.VSync.ToInt();
            Screen.fullScreen = level.FullScreen;
            Screen.SetResolution(level.Resolution.width, level.Resolution.height, Screen.fullScreen);
            refreshSettings();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)] // agressive inline
        public static void SetQuality(int index)
        {
            int wasVSync = QualitySettings.vSyncCount;
            AnisotropicFiltering wasAnisotropic = QualitySettings.anisotropicFiltering;

            QualitySettings.SetQualityLevel(index, true);

            QualitySettings.anisotropicFiltering = wasAnisotropic;
            QualitySettings.vSyncCount = wasVSync;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)] // agressive inline
        public static void SetFullScreen(bool boolean)
        {
            Screen.fullScreen = boolean;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)] // agressive inline
        public static void SetVSync(bool boolean)
        {
            QualitySettings.vSyncCount = boolean.ToInt();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)] // agressive inline
        public static void SetAnisotropic(bool value)
        {
            QualitySettings.anisotropicFiltering = (AnisotropicFiltering)(value ? 2 : 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)] // agressive inline
        public static void SetTexture(int index)
        {
            QualitySettings.masterTextureLimit = index;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)] // agressive inline
        public static void SetShadowQuality(int index)
        {
            QualitySettings.shadows = (ShadowQuality)index;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)] // agressive inline
        public static void SetShadowResolution(int index)
        {
            QualitySettings.shadowResolution = (ShadowResolution)index;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)] // agressive inline
        public static void SetAntialiasing(int index)
        {
            // antiAliasing is 0, 2, 4, 8
            //        index is 0, 1, 2, 3
            QualitySettings.antiAliasing = index > 0 ? 1 << index : 0;
        }

        /// <summary> Change to Custom Quality Level, changes Qualit yDropdown value and saves other setting values </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)] // agressive inline
        private static void SetToCustomLevel(int unusedParam)
        {
            if (customQuality.CustomQualityIndex < 0)
                throw new Exception("No 'Custom' quality level found. Create one in Edit -> Project Settings -> Quality -> Add Quality Level");
            if (QualitySettings.GetQualityLevel() == customQuality.CustomQualityIndex)
                return;

            TPQualityLevel savedLevel = GetCurrentQualityLevel();
            QualitySettings.SetQualityLevel(customQuality.CustomQualityIndex, true);
            SetQualityLevel(savedLevel);
            onCustomQualitySet();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)] // agressive inline
        private static void AddDropdownOptions(Dropdown dropdown, int startIndex, List<string> options)
        {
            dropdown.ClearOptions();
            dropdown.AddOptions(options);
            dropdown.value = startIndex;
            dropdown.RefreshShownValue();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)] // agressive inline
        private static void DropdownRefresher(Dropdown dropdown, Action action)
        {
            dropdown.onValueChanged.RemoveListener(SetToCustomLevel);
            action();
            dropdown.onValueChanged.AddListener(SetToCustomLevel);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)] // agressive inline
        private static void SetSliderValues(Slider slider, float startValue, float minValue, float maxValue)
        {
            slider.minValue = minValue;
            slider.maxValue = maxValue;
            slider.value = startValue;
        }
    }
}
