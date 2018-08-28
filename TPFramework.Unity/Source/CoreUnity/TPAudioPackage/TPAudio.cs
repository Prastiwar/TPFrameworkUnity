﻿/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity 
*/

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace TPFramework.Unity
{
    [Serializable]
    public struct TPAudioObject
    {
        public string Name;
        public AudioClip Clip;
    }

    public enum TPAudioSource
    {
        SFX,
        Theme
    }

    public static class TPAudio
    {
        private static Dictionary<string, TPAudioBundle> audioPool = new Dictionary<string, TPAudioBundle>();
        private static AudioSource sfxSource;
        private static AudioSource themeSource;

        /// <summary> Gets AudioSource from GameObject named "TPAudioSource"
        /// <para> On Get - returns source - if null, create it </para>
        /// <para> On Set - behaves like preset </para>
        /// </summary>
        public static AudioSource SFXSource {
            get {
                if (sfxSource is null)
                {
                    sfxSource = GetOrCreateSource("TPAduioSFXSource", false);
                }
                return sfxSource;
            }
            set {
                if (!(value is null))
                {
                    sfxSource = SFXSource;
                    CopySourceParemeters(value, ref sfxSource);
                }
            }
        }

        /// <summary> Gets AudioSource from GameObject named "TPAudioThemeSource"
        /// <para> On Get - returns source - if null, create it </para>
        /// <para> On Set - behaves like preset </para>
        /// </summary>
        public static AudioSource ThemeSource {
            get {
                if (themeSource is null)
                {
                    themeSource = GetOrCreateSource("TPAudioThemeSource", true);
                }
                return themeSource;
            }
            set {
                if (!(value is null))
                {
                    themeSource = ThemeSource;
                    CopySourceParemeters(value, ref themeSource);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddToPool(string bundleName, TPAudioBundle bundle)
        {
            audioPool[bundleName] = bundle;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddToPool(TPAudioBundle bundle)
        {
            AddToPool(bundle.name, bundle);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveFromPool(string bundleName)
        {
            audioPool.Remove(bundleName);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveFromPool(TPAudioBundle bundle)
        {
            audioPool.Remove(bundle.name);
        }

        /// <summary> Removes all Audio Bundles from pool </summary> 
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Dispose()
        {
            audioPool.Clear();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AudioSource SetClip(TPAudioBundle bundle, string audioName, TPAudioSource source = TPAudioSource.SFX)
        {
            AudioClip clip = GetClip(bundle, audioName);
            GetSource(source).clip = clip;
            return SFXSource;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AudioSource SetClip(string bundleName, string audioName, TPAudioSource source = TPAudioSource.SFX)
        {
            return SetClip(GetBundle(bundleName), audioName, source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Play(TPAudioBundle bundle, string audioName, TPAudioSource source = TPAudioSource.SFX, ulong delay = 0)
        {
            AudioClip clip = GetClip(bundle, audioName);
            GetSource(source).clip = clip;
            GetSource(source).Play(delay);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Play(string bundleName, string audioName, TPAudioSource source = TPAudioSource.SFX, ulong delay = 0)
        {
            Play(GetBundle(bundleName), audioName, source, delay);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void PlayOneShot(TPAudioBundle bundle, string audioName, float volumeScale = 1.0f)
        {
            SFXSource.PlayOneShot(GetClip(bundle, audioName), volumeScale);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void PlayOneShot(string bundleName, string audioName, float volumeScale = 1.0f)
        {
            PlayOneShot(GetBundle(bundleName), audioName, volumeScale);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void PlayOneShot(TPAudioBundle bundle, string audioName, Action onAudioEnd, float volumeScale = 1.0f)
        {
            AudioClip clip = GetClip(bundle, audioName);
            SFXSource.PlayOneShot(clip, volumeScale);
            Core.TPExtensions.DelayAction(clip.length, onAudioEnd);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void PlayOneShot(string bundleName, string audioName, Action onAudioEnd, float volumeScale = 1.0f)
        {
            PlayOneShot(GetBundle(bundleName), audioName, onAudioEnd, volumeScale);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Play(TPAudioBundle bundle, string audioName, Action onAudioEnd, TPAudioSource source = TPAudioSource.SFX, ulong delay = 0)
        {
            AudioClip clip = GetClip(bundle, audioName);
            GetSource(source).clip = clip;
            GetSource(source).Play(delay);
            Core.TPExtensions.DelayAction(clip.length + delay, onAudioEnd);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Play(string bundleName, string audioName, Action onAudioEnd, TPAudioSource source = TPAudioSource.SFX, ulong delay = 0)
        {
            Play(GetBundle(bundleName), audioName, onAudioEnd, source, delay);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AudioClip GetClip(TPAudioBundle bundle, string audioName)
        {
            int length = bundle.AudioObjects.Length;
            for (int i = 0; i < length; i++)
            {
                if (bundle.AudioObjects[i].Name == audioName)
                {
                    return bundle.AudioObjects[i].Clip;
                }
            }
            Debug.LogError("Audio clip named " + audioName + " in this bundle not found");
            return null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AudioClip GetClip(string bundleName, string audioName)
        {
            return GetClip(GetBundle(bundleName), audioName);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TPAudioBundle GetBundle(string bundleName)
        {
            return SafeKey(bundleName) ? audioPool[bundleName] : null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static AudioSource GetSource(TPAudioSource source)
        {
            return source == TPAudioSource.SFX ? SFXSource : ThemeSource;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static AudioSource GetOrCreateSource(string gameObjectName, bool loop)
        {
            GameObject obj = GameObject.Find(gameObjectName);
            return obj is null ? CreateNewSource(gameObjectName, loop) : obj.GetComponent<AudioSource>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasKey(string key)
        {
            return audioPool.ContainsKey(key);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static AudioSource CreateNewSource(string gameObjectName, bool loop)
        {
            GameObject newObj = new GameObject(gameObjectName);
            AudioSource audioSource = newObj.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.loop = loop;
            UnityEngine.Object.DontDestroyOnLoad(newObj);
            return audioSource;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void CopySourceParemeters(AudioSource fromSource, ref AudioSource toSource)
        {
            toSource.bypassEffects = fromSource.bypassEffects;
            toSource.bypassListenerEffects = fromSource.bypassListenerEffects;
            toSource.bypassReverbZones = fromSource.bypassReverbZones;
            toSource.dopplerLevel = fromSource.dopplerLevel;
            toSource.enabled = fromSource.enabled;
            toSource.hideFlags = fromSource.hideFlags;
            toSource.ignoreListenerPause = fromSource.ignoreListenerPause;
            toSource.ignoreListenerVolume = fromSource.ignoreListenerVolume;
            toSource.loop = fromSource.loop;
            toSource.maxDistance = fromSource.maxDistance;
            toSource.minDistance = fromSource.minDistance;
            toSource.mute = fromSource.mute;
            toSource.outputAudioMixerGroup = fromSource.outputAudioMixerGroup;
            toSource.panStereo = fromSource.panStereo;
            toSource.pitch = fromSource.pitch;
            toSource.playOnAwake = fromSource.playOnAwake;
            toSource.priority = fromSource.priority;
            toSource.reverbZoneMix = fromSource.reverbZoneMix;
            toSource.rolloffMode = fromSource.rolloffMode;
            toSource.spatialBlend = fromSource.spatialBlend;
            toSource.spatialize = fromSource.spatialize;
            toSource.spatializePostEffects = fromSource.spatializePostEffects;
            toSource.spread = fromSource.spread;
            toSource.tag = fromSource.tag;
            toSource.time = fromSource.time;
            toSource.timeSamples = fromSource.timeSamples;
            toSource.velocityUpdateMode = fromSource.velocityUpdateMode;
            toSource.volume = fromSource.volume;
            toSource.SetCustomCurve(AudioSourceCurveType.CustomRolloff, fromSource.GetCustomCurve(AudioSourceCurveType.CustomRolloff));
            toSource.SetCustomCurve(AudioSourceCurveType.ReverbZoneMix, fromSource.GetCustomCurve(AudioSourceCurveType.ReverbZoneMix));
            toSource.SetCustomCurve(AudioSourceCurveType.SpatialBlend, fromSource.GetCustomCurve(AudioSourceCurveType.SpatialBlend));
            toSource.SetCustomCurve(AudioSourceCurveType.Spread, fromSource.GetCustomCurve(AudioSourceCurveType.Spread));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool SafeKey(string key)
        {
            if (HasKey(key))
            {
                return true;
            }
            Debug.LogError("This key doesn't exist: " + key);
            return false;
        }
    }
}
