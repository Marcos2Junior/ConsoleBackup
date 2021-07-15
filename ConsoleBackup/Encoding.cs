using System;
using System.Text;
using System.Security.Cryptography;

namespace ConsoleBackup
{
    public class Encoding
    {
        public static string Encrypt(string text)
        {
            return Convert.ToBase64String(TransformFinalBlock(Transform(TypeEncoding.Encrypt), new UTF8Encoding().GetBytes(text)));
        }

        public static string Decrypt(string textCript)
        {
            return new UTF8Encoding().GetString(TransformFinalBlock(Transform(TypeEncoding.Decrypt), Convert.FromBase64String(textCript)));
        }

        private static byte[] TransformFinalBlock(ICryptoTransform cryptoTransform, byte[] data)
        {
            return cryptoTransform.TransformFinalBlock(data, 0, data.Length);
        }

        private static ICryptoTransform Transform(TypeEncoding typeEncoding)
        {
            using (MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider())
            {
                using (TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider()
                {
                    Key = HashProvider.ComputeHash(new UTF8Encoding().GetBytes("key")),
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7
                })
                {
                    return typeEncoding == TypeEncoding.Encrypt ? TDESAlgorithm.CreateEncryptor() : TDESAlgorithm.CreateDecryptor();
                }
            }
        }

        enum TypeEncoding
        {
            Encrypt,
            Decrypt
        }
    }
}
