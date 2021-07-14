using System;
using System.Text;
using System.Security.Cryptography;

namespace ConsoleBackup
{
    public class Encoding
    {
        private const string key = "key";
        public static string Encrypt(string text)
        {
            byte[] Results;
            using (MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider())
            {
                using (TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider()
                {
                    Key = HashProvider.ComputeHash(new UTF8Encoding().GetBytes(key)),
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7
                })
                {
                    ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
                    byte[] DataToEncrypt = new UTF8Encoding().GetBytes(text);
                    Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
                }
            }
            return Convert.ToBase64String(Results);
        }

        public static string Decrypt(string textCript)
        {
            byte[] Results;
            using (MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider())
            {
                using (TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider()
                {
                    Key = HashProvider.ComputeHash(new UTF8Encoding().GetBytes(key)),
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7
                })
                {
                    ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
                    byte[] DataToDecrypt = Convert.FromBase64String(textCript);
                    Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
                }
            }
            return new UTF8Encoding().GetString(Results);
        }
    }
}
