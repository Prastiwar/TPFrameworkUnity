/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity 
*/

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TPFramework.Core;
using UnityEngine;

namespace TPFramework.Unity
{
    public class TPPersistPrefs : TPPersistSystem<TPPersistPrefs>
    {
        private static readonly string cryptKey = "80D30A1C969CB772B1FDF7F077304D486AEE15541CA78DF7E7DE24F412FBB97C";

        private static readonly HashSet<Type> supportedTypes = new HashSet<Type>() {
            typeof(int),
            typeof(float),
            typeof(string),
            typeof(bool)
        };

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override HashSet<Type> GetSupportedTypes() { return supportedTypes; }

        /// <summary> Called on Load() for field with PersistantAttribute </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override object LoadValue(PersistantAttribute attribute, Type objectType)
        {
            string decrypt = Decrypt(PlayerPrefs.GetString(Encrypt(attribute.Key)));
            return string.IsNullOrEmpty(decrypt)
                ? Convert.ChangeType(attribute.DefaultValue ?? decrypt, objectType)
                : Convert.ChangeType(decrypt, objectType);
        }

        /// <summary> Called on Save() for field with PersistantAttribute </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void SaveValue(PersistantAttribute attribute, object saveValue)
        {
            PlayerPrefs.SetString(Encrypt(attribute.Key), Encrypt(saveValue.ToString()));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private string Encrypt(string value)
        {
            return Cryptography.Encrypt(value, cryptKey);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private string Decrypt(string cryptedValue)
        {
            return string.IsNullOrEmpty(cryptedValue)
                ? string.Empty
                : Cryptography.Decrypt(cryptedValue, cryptKey);
        }
    }
}
