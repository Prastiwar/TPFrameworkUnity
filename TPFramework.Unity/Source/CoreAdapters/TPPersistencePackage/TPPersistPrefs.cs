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
using TPFramework.Core;
using UnityEngine;

namespace TPFramework.Unity
{
    public class TPPersistPrefs : TPPersistSystem<TPPersistPrefs>
    {
        private static readonly RSACryptoServiceProvider provider = GetOrSetProvider();

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
                decrypt = null;
                Debug.Log(attribute.DefaultValue ?? decrypt);
                return Convert.ChangeType(attribute.DefaultValue ?? decrypt, objectType);
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
            byte[] encryptBytes = provider.Encrypt(Encoding.UTF8.GetBytes(value), false);
            return Convert.ToBase64String(encryptBytes);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private string Decrypt(string cryptedValue)
        {
            string decrypted = string.Empty;
            if (string.IsNullOrEmpty(cryptedValue))
            {
                return decrypted;
            }
            decrypted = Encoding.UTF8.GetString(provider.Decrypt(Convert.FromBase64String(cryptedValue), false));
            return decrypted;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static RSACryptoServiceProvider GetOrSetProvider()
        {
            // This is the key used to encrypt and decrypt can be anything
            string importantKey = "80D30A1C969CB772B1FDF7F077304D486AEE15541CA78DF7E7DE24F412FBB97C";

            CspParameters crpyter = new CspParameters { KeyContainerName = importantKey, };
            RSACryptoServiceProvider provider = new RSACryptoServiceProvider(crpyter) { PersistKeyInCsp = true };

            if (PlayerPrefs.HasKey(importantKey))
            {
                provider.FromXmlString(PlayerPrefs.GetString(importantKey));
            }
            else
            {
                PlayerPrefs.SetString(importantKey, provider.ToXmlString(true));
            }
            return provider;
        }
    }
}
