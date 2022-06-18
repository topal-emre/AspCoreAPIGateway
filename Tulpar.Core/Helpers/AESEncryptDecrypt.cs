using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Tulpar.Core
{
    public static class AESEncryptDecrypt<T>
    {
        /// <summary>
        /// Must be 16 characters long
        /// </summary>
        private static readonly string AES_Private_Key = "Tulpar.Core_2020";

        public static T DecryptStringAES(string cipherText)
        {
            byte[] keybytes = Encoding.UTF8.GetBytes(AES_Private_Key);
            byte[] iv = Encoding.UTF8.GetBytes(AES_Private_Key);
            byte[] encrypted = System.Convert.FromBase64String(cipherText);
            string decriptedFromJavascript = DecryptStringFromBytes(encrypted, keybytes, iv);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(System.Convert.FromBase64String(decriptedFromJavascript)));
        }
        public static string DecryptStringFromBytes(byte[] cipherText, byte[] key, byte[] iv)
        {
            if (cipherText == null || cipherText.Length <= 0)
            {
                throw new ArgumentNullException("cipherText");
            }
            if (key == null || key.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }
            if (iv == null || iv.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }

            string plaintext = null;

            // Create my RijndaelManaged object
            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                //Settingleri yap
                rijAlg.Mode = CipherMode.CBC;
                rijAlg.Padding = PaddingMode.PKCS7;
                rijAlg.FeedbackSize = 128;

                rijAlg.Key = key;
                rijAlg.IV = iv;

                ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);
                try
                {
                    using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {

                            using (StreamReader srDecrypt = new StreamReader(csDecrypt, Encoding.GetEncoding("iso-8859-9")))
                            {
                                plaintext = srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }
                catch
                {
                    plaintext = "keyError";
                }
            }

            return plaintext;
        }
        public static string EncryptStringAES(string cipherText)
        {
            byte[] keybytes = Encoding.UTF8.GetBytes(AES_Private_Key);
            byte[] iv = Encoding.UTF8.GetBytes(AES_Private_Key);
            byte[] encrypted = System.Convert.FromBase64String(cipherText);
            byte[] decriptedFromJavascript = EncryptStringToBytes(cipherText, keybytes, iv);
            string converted = System.Convert.ToBase64String(decriptedFromJavascript);
            return converted;
        }
        public static byte[] EncryptStringToBytes(string plainText, byte[] key, byte[] iv)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
            {
                throw new ArgumentNullException("plainText");
            }
            if (key == null || key.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }
            if (iv == null || iv.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }
            byte[] encrypted;
            // Create a RijndaelManaged object
            // with the specified key and IV.
            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                rijAlg.Mode = CipherMode.CBC;
                rijAlg.Padding = PaddingMode.PKCS7;
                rijAlg.FeedbackSize = 128;

                rijAlg.Key = key;
                rijAlg.IV = iv;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt, Encoding.GetEncoding("iso-8859-9")))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            // Return the encrypted bytes from the memory stream.
            return encrypted;
        }

    }
}
