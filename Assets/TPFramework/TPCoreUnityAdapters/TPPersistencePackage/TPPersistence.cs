/**
*   Authored by Tomasz Piowczyk
*   License: https://github.com/Prastiwar/TPFramework/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFramework 
*/

using System;
using System.Collections.Generic;
using UnityEngine;
using TPFramework.Core;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

namespace TPFramework.Unity
{
    public class TPPersistantPrefs : TPPersistant
    {
        private static readonly CspParameters crpyter = new CspParameters {
            KeyContainerName = "ThisIsAKey"  // This is the key used to encrypt and decrypt can be anything
        };

        private static readonly RSACryptoServiceProvider provider = new RSACryptoServiceProvider(crpyter);

        private static readonly HashSet<Type> supportedTypes = new HashSet<Type>() {
            typeof(int),
            typeof(float),
            typeof(string),
            typeof(bool)
        };

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void PersistantPrefs()
        {
            Instance = new TPPersistantPrefs(); // need to set instance to use static methods getting values from this script
        }

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        protected override HashSet<Type> GetSupportedTypes() { return supportedTypes; }

        /// <summary> Called on Load() for field with PersistantAttribute </summary>
        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        protected override object LoadValue(PersistantAttribute attribute, object objectValue)
        {
            string decrypt = Decrypt(PlayerPrefs.GetString(attribute.Key));
            if (string.IsNullOrEmpty(decrypt))
            {
                return attribute.DefaultValue ?? null;
            }
            return Convert.ChangeType(decrypt, objectValue.GetType());
        }

        /// <summary> Called on Save() for field with PersistantAttribute </summary>
        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        protected override void SaveValue(PersistantAttribute attribute, object saveValue)
        {
            PlayerPrefs.SetString(attribute.Key, Encrypt(saveValue.ToString()));
        }

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        private string Encrypt(string value)
        {
            byte[] encryptBytes = provider.Encrypt(Encoding.UTF8.GetBytes(value), true);
            return Convert.ToBase64String(encryptBytes);
        }

        [MethodImpl((MethodImplOptions)0x100)] // agressive inline
        private string Decrypt(string cryptedValue)
        {
            return string.IsNullOrEmpty(cryptedValue) 
                ? string.Empty 
                : Encoding.UTF8.GetString(provider.Decrypt(Convert.FromBase64String(cryptedValue), true));
        }
    }
}
