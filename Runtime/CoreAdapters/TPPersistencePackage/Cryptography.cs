using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

namespace TP.Framework.Unity
{
    public static class Cryptography
    {
        private static readonly int iterations = 2;
        private static readonly int keySize = 256;

        private static readonly string hash     = "SHA1";
        private static readonly string salt     = "f0a09fdsldfs983h";
        private static readonly string vector   = "0dmv74ygjtows3jk";

        private static readonly byte[] vectorBytes;
        private static readonly byte[] saltBytes;

        private static string cachedPassword = "80D30A1C969CB772B1FDF7F077304D486AEE15541CA78DF7E7DE24F412FBB97C";
        private static PasswordDeriveBytes passwordBytes;
        private static byte[] keyBytes;

        static Cryptography()
        {
            vectorBytes = Encoding.ASCII.GetBytes(vector);
            saltBytes = Encoding.ASCII.GetBytes(salt);
            SetPassword();
        }

        private static void SetPassword(string password = "")
        {
            if (cachedPassword != password)
            {
                cachedPassword = password;
                passwordBytes = new PasswordDeriveBytes(cachedPassword, saltBytes, hash, iterations);
                keyBytes = passwordBytes.GetBytes(keySize / 8);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Encrypt(string value, string password)
        {
            return Encrypt<AesManaged>(value, password);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Decrypt(string value, string password)
        {
            return Decrypt<AesManaged>(value, password);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Encrypt<T>(string value, string password) where T : SymmetricAlgorithm, new()
        {
            SetPassword(password);
            byte[] valueBytes = Encoding.UTF8.GetBytes(value);
            byte[] encrypted = null;

            using (T alghoritm = new T() { Mode = CipherMode.CBC })
            {
                using (ICryptoTransform encryptor = alghoritm.CreateEncryptor(keyBytes, vectorBytes))
                {
                    using (MemoryStream stream = new MemoryStream())
                    {
                        using (CryptoStream writer = new CryptoStream(stream, encryptor, CryptoStreamMode.Write))
                        {
                            writer.Write(valueBytes, 0, valueBytes.Length);
                            writer.FlushFinalBlock();
                            encrypted = stream.ToArray();
                        }
                    }

                }
                alghoritm.Clear();
            }
            return Convert.ToBase64String(encrypted);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Decrypt<T>(string value, string password) where T : SymmetricAlgorithm, new()
        {
            SetPassword(password);
            byte[] valueBytes = Convert.FromBase64String(value);
            byte[] decrypted = null;
            int decryptedByteCount = 0;

            using (T alghoritm = new T() { Mode = CipherMode.CBC })
            {
                try
                {
                    using (ICryptoTransform decryptor = alghoritm.CreateDecryptor(keyBytes, vectorBytes))
                    {
                        using (MemoryStream stream = new MemoryStream(valueBytes))
                        {
                            using (CryptoStream reader = new CryptoStream(stream, decryptor, CryptoStreamMode.Read))
                            {
                                decrypted = new byte[valueBytes.Length];
                                decryptedByteCount = reader.Read(decrypted, 0, decrypted.Length);
                                reader.Clear();
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    return string.Empty;
                }
                alghoritm.Clear();
            }
            return Encoding.UTF8.GetString(decrypted, 0, decryptedByteCount);
        }
    }
}
