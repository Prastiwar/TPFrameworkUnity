/**
*   Authored by Tomasz Piowczyk
*   MIT LICENSE: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity 
*/

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using TPFramework.Core;

namespace TPFramework.Unity
{
    public class TPPersistPrefs : TPPersistSystem<TPPersistPrefs>
    {
        private static readonly RSACryptoServiceProvider provider = new RSACryptoServiceProvider(crpyter);

        private static readonly CspParameters crpyter = new CspParameters {
            KeyContainerName = "ThisIsAKey"  // This is the key used to encrypt and decrypt can be anything
        };

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
            string decrypt = Decrypt(PlayerPrefs.GetString(attribute.Key));
            if (string.IsNullOrEmpty(decrypt))
            {
                return attribute.DefaultValue ?? null;
            }
            return Convert.ChangeType(decrypt, objectType);
        }

        /// <summary> Called on Save() for field with PersistantAttribute </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void SaveValue(PersistantAttribute attribute, object saveValue)
        {
            PlayerPrefs.SetString(attribute.Key, Encrypt(saveValue.ToString()));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private string Encrypt(string value)
        {
            byte[] encryptBytes = provider.Encrypt(Encoding.UTF8.GetBytes(value), true);
            return Convert.ToBase64String(encryptBytes);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private string Decrypt(string cryptedValue)
        {
            return string.IsNullOrEmpty(cryptedValue) 
                ? string.Empty 
                : Encoding.UTF8.GetString(provider.Decrypt(Convert.FromBase64String(cryptedValue), true));
        }
    }
}
