using System;
using System.Security.Cryptography;
using System.Text;

namespace Siscred.Infra.CrossCutting.Helpers
{
    public class PasswordEncryption
    {
        public static string ComputeHash(string plainText, string hashAlgorithm, byte[] saltBytes)
        {
            if (saltBytes == null)
            {
                const int minSaltSize = 4;
                const int maxSaltSize = 8;

                var random = new Random();
                var saltSize = random.Next(minSaltSize, maxSaltSize);

                saltBytes = new byte[saltSize];

                var rng = new RNGCryptoServiceProvider();

                rng.GetNonZeroBytes(saltBytes);
            }

            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            var plainTextWithSaltBytes = new byte[plainTextBytes.Length + saltBytes.Length];

            for (var i = 0; i < plainTextBytes.Length; i++)
                plainTextWithSaltBytes[i] = plainTextBytes[i];

            for (var i = 0; i < saltBytes.Length; i++)
                plainTextWithSaltBytes[plainTextBytes.Length + i] = saltBytes[i];

            HashAlgorithm hash;

            if (hashAlgorithm == null) hashAlgorithm = "";

            switch (hashAlgorithm.ToUpper())
            {
                case "SHA1":
                    hash = new SHA1Managed();
                    break;

                case "SHA256":
                    hash = new SHA256Managed();
                    break;

                case "SHA384":
                    hash = new SHA384Managed();
                    break;

                case "SHA512":
                    hash = new SHA512Managed();
                    break;

                default:
                    hash = new MD5CryptoServiceProvider();
                    break;
            }

            var hashBytes = hash.ComputeHash(plainTextWithSaltBytes);

            var hashWithSaltBytes = new byte[hashBytes.Length + saltBytes.Length];

            for (var i = 0; i < hashBytes.Length; i++)
                hashWithSaltBytes[i] = hashBytes[i];

            for (var i = 0; i < saltBytes.Length; i++)
                hashWithSaltBytes[hashBytes.Length + i] = saltBytes[i];

            var hashValue = Convert.ToBase64String(hashWithSaltBytes);

            return hashValue;
        }

        public static bool VerifyHash(string plainText, string hashAlgorithm, string hashValue)
        {
            var hashWithSaltBytes = Convert.FromBase64String(hashValue);

            int hashSizeInBits;

            if (hashAlgorithm == null)
                hashAlgorithm = "";

            switch (hashAlgorithm.ToUpper())
            {
                case "SHA1":
                    hashSizeInBits = 160;
                    break;

                case "SHA256":
                    hashSizeInBits = 256;
                    break;

                case "SHA384":
                    hashSizeInBits = 384;
                    break;

                case "SHA512":
                    hashSizeInBits = 512;
                    break;

                default: // MD5
                    hashSizeInBits = 128;
                    break;
            }

            var hashSizeInBytes = hashSizeInBits / 8;

            if (hashWithSaltBytes.Length < hashSizeInBytes) return false;

            var saltBytes = new byte[hashWithSaltBytes.Length - hashSizeInBytes];

            for (var i = 0; i < saltBytes.Length; i++)
                saltBytes[i] = hashWithSaltBytes[hashSizeInBytes + i];

            var expectedHashString = ComputeHash(plainText, hashAlgorithm, saltBytes);

            return hashValue == expectedHashString;
        }
    }
}